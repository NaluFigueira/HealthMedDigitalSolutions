using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;

namespace PosTech.Hackathon.Appointments.Api.EndPoints;

public static class DoctorEndpoint
{
    public static void MapDoctorEndpoints(this WebApplication app)
    {
        var tags = new List<OpenApiTag> { new() { Name = "Doctors Appointments" } };

        app.MapGet("/doctor/appointments/pending", [Authorize] (
                HttpContext httpContext,
                IGetPendingAppointmentsUseCase getPendingAppointmentsUseCase) =>
            GetPendingDoctorsAppointments(httpContext, getPendingAppointmentsUseCase))
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Tags = tags,
                Summary = "Get pending appointments for a doctor",
                Description = "Retrieves all pending appointments for the authenticated doctor."
            })
            .Produces<List<PendingAppointmentsDTO>>()
            .Produces<string>(StatusCodes.Status401Unauthorized)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();
    }

    private static async Task<IResult> GetPendingDoctorsAppointments(HttpContext httpContext, IGetPendingAppointmentsUseCase getPendingAppointmentsUseCase)
    {
        var doctorIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        if (string.IsNullOrEmpty(doctorIdClaim) || !Guid.TryParse(doctorIdClaim, out var doctorId))
            return Results.Unauthorized();

        var pendingAppointments = await getPendingAppointmentsUseCase.ExecuteAsync(doctorId);

        return Results.Ok(pendingAppointments.Value);
    }
}
