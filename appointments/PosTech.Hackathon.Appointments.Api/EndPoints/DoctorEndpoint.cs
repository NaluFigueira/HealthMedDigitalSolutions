using FluentResults;

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using PosTech.Hackathon.Appointments.Api.Utils;
using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using System.Text.Json;

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
    }
}
