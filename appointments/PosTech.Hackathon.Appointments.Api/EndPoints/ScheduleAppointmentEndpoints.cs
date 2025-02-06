using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;

namespace PosTech.Hackathon.Appointments.Api.EndPoints;

public static class ScheduleAppointmentEndpoints
{
    public static void MapScheduleAppointmentEndpoints(this WebApplication app)
    {
        var tags = new List<OpenApiTag> { new() { Name = "Schedule Appointment" } };

        app.MapPost("/appointment/schedule", [Authorize]
        (
                        HttpContext httpContext,
                        [FromBody] ScheduleAppointmentDTO request,
                        [FromServices] IScheduleAppointmentUseCase scheduleAppointmentUseCase) =>
                    ScheduleAppointment(httpContext, scheduleAppointmentUseCase, request))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Schedule a medical appointment",
                Description =
                    "Schedules a appointment for an authenticated patient with a selected doctor and time.",
                RequestBody = new OpenApiRequestBody
                {
                    Required = true,
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new()
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["doctorCrm"] =
                                        new() { Type = "string", Description = "Doctor's CRM" },
                                    ["scheduledTime"] =
                                        new()
                                        {
                                            Type = "string",
                                            Format = "date-time",
                                            Description = "Scheduled appointment time"
                                        }
                                },
                                Required = new HashSet<string> { "doctorCrm", "scheduledTime" }
                            }
                        }
                    }
                }
            })
            .Produces(StatusCodes.Status202Accepted)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status401Unauthorized)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();
    }

    private static async Task<IResult> ScheduleAppointment(
        HttpContext httpContext,
        IScheduleAppointmentUseCase scheduleAppointmentUseCase,
        ScheduleAppointmentDTO request)
    {
        var patientIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        if (string.IsNullOrEmpty(patientIdClaim) || !Guid.TryParse(patientIdClaim, out var patientId))
            return Results.Unauthorized();

        var result = await scheduleAppointmentUseCase.ExecuteAsync(patientId, request);

        return result.IsSuccess ? Results.Accepted() : Results.BadRequest(result.Errors);
    }
}

