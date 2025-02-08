using Microsoft.OpenApi.Models;
using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;

namespace PosTech.Hackathon.Appointments.Api.EndPoints;

public static class DoctorsAppointmentEndPoint
{
    public static void MapDoctorsAppointmentEndpoints(this WebApplication app)
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

        app.MapPost("/doctor/appointments/{appointmentId}/reject", [Authorize] (
                HttpContext httpContext,
                IRejectAppointmentUseCase rejectAppointmentUseCase,
                Guid appointmentId) => RejectDoctorsAppointments(httpContext, rejectAppointmentUseCase, appointmentId))
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Tags = tags,
                Summary = "Reject an appointment",
                Description = "Rejects an appointment request."
            })
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status401Unauthorized)
            .RequireAuthorization();


        app.MapPost("/doctor/appointments/{appointmentId}/accept", [Authorize] (
                HttpContext httpContext,
                IAcceptAppointmentUseCase acceptAppointmentUseCase,
                Guid appointmentId) => AcceptDoctorsAppointments(httpContext, acceptAppointmentUseCase, appointmentId))
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Tags = tags,
                Summary = "Accept an appointment",
                Description = "Accepts an appointment request."
            })
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status401Unauthorized)
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

    private static async Task<IResult> RejectDoctorsAppointments(HttpContext httpContext, IRejectAppointmentUseCase rejectAppointmentUseCase, Guid appointmentId)
    {
        var doctorIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        if (string.IsNullOrEmpty(doctorIdClaim) || !Guid.TryParse(doctorIdClaim, out var doctorId))
            return Results.Unauthorized();

        await rejectAppointmentUseCase.ExecuteAsync(doctorId, appointmentId);

        return Results.Ok("Appointment rejected successfully.");
    }

    private static async Task<IResult> AcceptDoctorsAppointments(HttpContext httpContext, IAcceptAppointmentUseCase acceptAppointmentUseCase, Guid appointmentId)
    {
        var doctorIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        if (string.IsNullOrEmpty(doctorIdClaim) || !Guid.TryParse(doctorIdClaim, out var doctorId))
            return Results.Unauthorized();

        await acceptAppointmentUseCase.ExecuteAsync(doctorId, appointmentId);

        return Results.Ok("Appointment accepted successfully.");

    }
}
