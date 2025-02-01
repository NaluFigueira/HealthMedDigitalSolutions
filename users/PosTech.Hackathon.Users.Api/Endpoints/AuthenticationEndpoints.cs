using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using PosTech.Hackathon.Users.Api.Utils;
using PosTech.Hackathon.Users.Application.DTOs;
using PosTech.Hackathon.Users.Application.Interfaces.UseCases;

namespace PosTech.Hackathon.Users.Api.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this WebApplication app)
    {
        var tags = new List<OpenApiTag> { new() { Name = "Authentication" } };

        app.MapPost("/login", [AllowAnonymous] ([FromBody] LoginDTO dto, ILoginUseCase useCase) => LogIn(dto, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Log in into the application",
                RequestBody = new OpenApiRequestBody()
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Example = new OpenApiString(JsonSerializer.Serialize(new LoginDTO()
                            {
                                UserName = "default_doctor",
                                Password = "S3cur3P@ssW0rd",
                            })),
                        }
                    }
                }
            })
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> LogIn(LoginDTO dto, ILoginUseCase loginUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await loginUseCase.ExecuteAsync(dto);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }
}
