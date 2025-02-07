
﻿using FluentResults;

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using PosTech.Hackathon.Appointments.Api.Utils;
using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using System.Text.Json;

﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;


namespace PosTech.Hackathon.Appointments.Api.EndPoints;

public static class DoctorEndpoint
{
    public static void MapDoctorEndpoints(this WebApplication app)
    {

        var tags = new List<OpenApiTag> { new() { Name = "Doctor" } };
        
        app.MapGet("/doctors", (string speciality, IGetDoctorsUseCase getDoctorsUseCase) =>
                GetDoctors(speciality, getDoctorsUseCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Returns a list of registered doctors"
            })
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);

        app.MapGet("/doctors/availability", (Guid doctorId, IGetAvailabilitySlotsUseCase getAvailabilitySlotsUseCase) =>
                GetAvailabilitySlot(doctorId, getAvailabilitySlotsUseCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Returns a list of available times for doctors"
            })
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);

    }

    private static async Task<IResult> GetDoctors(string speciality, IGetDoctorsUseCase getDoctorsUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await getDoctorsUseCase.ExecuteAsync(speciality);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }

    private static async Task<IResult> GetAvailabilitySlot(Guid speciality, IGetAvailabilitySlotsUseCase getAvailabilityUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await getAvailabilityUseCase.ExecuteAsync(speciality);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });

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
