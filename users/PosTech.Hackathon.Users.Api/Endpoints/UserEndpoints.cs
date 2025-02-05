using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using PosTech.Hackathon.Users.Api.Utils;
using PosTech.Hackathon.Users.Application.DTOs;
using PosTech.Hackathon.Users.Application.Interfaces.UseCases;

namespace PosTech.Hackathon.Users.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var tags = new List<OpenApiTag> { new() { Name = "Users" } };

        app.MapPost("/doctors", [AllowAnonymous] ([FromBody] CreateDoctorDTO dto, ICreateDoctorUseCase useCase) => CreateDoctor(dto, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Register doctor",
                RequestBody = new OpenApiRequestBody()
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Example = new OpenApiString(JsonSerializer.Serialize(new CreateDoctorDTO()
                            {
                                UserName = "default_doctor",
                                Email = "default_doctor_user@email.com",
                                Password = "S3cur3P@ssW0rd",
                                RePassword = "S3cur3P@ssW0rd",
                                Name = "Doctor Default",
                                CPF = "447.137.070-74",
                                CRM = "123456-SP",
                                AppointmentValue = 150,
                                Specialty = "Specialty",
                            })),
                        }
                    }
                }
            })
            .Produces(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);

        app.MapPost("/patients", [AllowAnonymous] ([FromBody] CreatePatientDTO dto, ICreatePatientUseCase useCase) => CreatePatient(dto, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Register patient",
                RequestBody = new OpenApiRequestBody()
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Example = new OpenApiString(JsonSerializer.Serialize(new CreatePatientDTO()
                            {
                                UserName = "default_patient",
                                Email = "default_patient_user@email.com",
                                Password = "S3cur3P@ssW0rd",
                                RePassword = "S3cur3P@ssW0rd",
                                Name = "Patient Default",
                                CPF = "480.145.250-78",
                            })),
                        }
                    }
                }
            })
            .Produces(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> CreateDoctor(CreateDoctorDTO dto, ICreateDoctorUseCase createDoctorUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await createDoctorUseCase.ExecuteAsync(dto);
            return result.IsSuccess ? Results.Created() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }

    private static async Task<IResult> CreatePatient(CreatePatientDTO dto, ICreatePatientUseCase createPatientUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await createPatientUseCase.ExecuteAsync(dto);
            return result.IsSuccess ? Results.Created() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }
}
