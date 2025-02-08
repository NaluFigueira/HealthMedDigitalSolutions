using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using PosTech.Hackathon.Appointments.Api.Utils;
using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;

namespace PosTech.Hackathon.Appointments.Api.Endpoints;

public static class AppointmentsEndpoints
{
    public static void MapAppointmentsEndpoints(this WebApplication app)
    {
        var tags = new List<OpenApiTag> { new() { Name = "Appointments" } };

        app.MapGet("/patient/{patientId}/appointments", ([FromRoute] Guid patientId, IGetAppointmentsUseCase useCase) => GetAppointments(new GetAppointmentsDTO { PatientId = patientId }, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Fetch a patient's appointment with a doctor"
            })
            .Produces(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);

        app.MapPost("/patient/{patientId}/appointment/{appointmentId}/cancel", ([FromRoute] Guid patientId, [FromRoute] Guid appointmentId, [FromBody] CancelAppointmentDTO cancelAppointmentDTO, ICancelAppointmentUseCase useCase) => CancelAppointment(new CancelAppointmentDTO { PatientId = patientId, AppointmentId = appointmentId, CancellationReason = cancelAppointmentDTO.CancellationReason }, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Cancel a patient appointment",
                RequestBody = new OpenApiRequestBody()
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Example = new OpenApiString(JsonSerializer.Serialize(new CancelAppointmentDTO
                            {
                                CancellationReason = "I'm not feeling well"
                            })),
                        }
                    }
                }
            })
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> GetAppointments(GetAppointmentsDTO dto, IGetAppointmentsUseCase getAppointmentsUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await getAppointmentsUseCase.ExecuteAsync(dto);
            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }

    private static async Task<IResult> CancelAppointment(CancelAppointmentDTO dto, ICancelAppointmentUseCase removeAvailabilitySlotsUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await removeAvailabilitySlotsUseCase.ExecuteAsync(dto);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }
}
