using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using PosTech.Hackathon.Appointments.Api.Utils;
using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;

namespace PosTech.Hackathon.Appointments.Api.Endpoints;

public static class AvailabilitySlotsEndpoints
{
    public static void MapAvailabilitySlotsEndpoints(this WebApplication app)
    {
        var tags = new List<OpenApiTag> { new() { Name = "Appointments" } };

        app.MapPost("/availabilitySlots", ([FromBody] AddAvailabilitySlotsDTO dto, IAddAvailabilitySlotsUseCase useCase) => AddAvailabilitySlots(dto, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Register doctor's availability slots",
                RequestBody = new OpenApiRequestBody()
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {

                            Example = new OpenApiString(JsonSerializer.Serialize(new AddAvailabilitySlotsDTO()
                            {
                                DoctorId = Guid.NewGuid(),
                                AvailabilitySlots = [
                                   new AvailabilitySlotDTO(DateTime.Now),
                                   new AvailabilitySlotDTO(DateTime.Now.AddHours(1)),
                                   new AvailabilitySlotDTO(DateTime.Now.AddHours(2))
                                ]
                            })),
                        }
                    }
                }
            })
            .Produces(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> AddAvailabilitySlots(AddAvailabilitySlotsDTO dto, IAddAvailabilitySlotsUseCase addAvailabilitySlotsUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await addAvailabilitySlotsUseCase.ExecuteAsync(dto);
            return result.IsSuccess ? Results.Created() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }

    //app.MapPost("/patients", [AllowAnonymous] ([FromBody] CreatePatientDTO dto, ICreatePatientUseCase useCase) => CreatePatient(dto, useCase))
    //    .WithOpenApi(operation => new(operation)
    //    {
    //        Tags = tags,
    //        Summary = "Register patient",
    //        RequestBody = new OpenApiRequestBody()
    //        {
    //            Content = new Dictionary<string, OpenApiMediaType>
    //            {
    //                ["application/json"] = new OpenApiMediaType
    //                {
    //                    Example = new OpenApiString(JsonSerializer.Serialize(new CreatePatientDTO()
    //                    {
    //                        UserName = "default_patient",
    //                        Email = "default_patient_user@email.com",
    //                        Password = "S3cur3P@ssW0rd",
    //                        RePassword = "S3cur3P@ssW0rd",
    //                        Name = "Patient Default",
    //                        CPF = "480.145.250-78",
    //                    })),
    //                }
    //            }
    //        }
    //    })
    //    .Produces(StatusCodes.Status201Created)
    //    .Produces<string>(StatusCodes.Status400BadRequest)
    //    .Produces<string>(StatusCodes.Status500InternalServerError);
}

//private static async Task<IResult> CreateDoctor(CreateDoctorDTO dto, ICreateDoctorUseCase createDoctorUseCase)
//{
//    return await EndpointUtils.CallUseCase(async () =>
//    {
//        var result = await createDoctorUseCase.ExecuteAsync(dto);
//        return result.IsSuccess ? Results.Created() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
//    });
//}

//private static async Task<IResult> CreatePatient(CreatePatientDTO dto, ICreatePatientUseCase createPatientUseCase)
//{
//    return await EndpointUtils.CallUseCase(async () =>
//    {
//        var result = await createPatientUseCase.ExecuteAsync(dto);
//        return result.IsSuccess ? Results.Created() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
//    });
//}
//}
