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
        var tags = new List<OpenApiTag> { new() { Name = "AvailabilitySlots" } };

        app.MapPost("/doctor/{doctorId}/availability", ([FromRoute] Guid doctorId, [FromBody] AddAvailabilitySlotsDTO dto, IAddAvailabilitySlotsUseCase useCase) => AddAvailabilitySlots(new AddAvailabilitySlotsDTO { DoctorId = doctorId, AvailabilitySlots = dto.AvailabilitySlots }, useCase))
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

                            Example = new OpenApiString(JsonSerializer.Serialize(new AddAvailabilitySlotsDTO
                            {
                                AvailabilitySlots = [
                                   new AvailabilitySlotDTO { Slot = DateTime.Now},
                                   new AvailabilitySlotDTO { Slot = DateTime.Now.AddHours(1) },
                                   new AvailabilitySlotDTO { Slot = DateTime.Now.AddHours(2) }
                                ]
                            })),
                        }
                    }
                }
            })
            .Produces(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);

        app.MapDelete("/doctor/{doctorId}/availability/{slotId}", ([FromRoute] Guid doctorId, [FromRoute] Guid slotId, IRemoveAvailabilitySlotsUseCase useCase) => RemoveAvailabilitySlots(new RemoveAvailabilitySlotsDTO { DoctorId = doctorId, SlotId = slotId }, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Remove a doctor's availability slots"
            })
            .Produces(StatusCodes.Status200OK)
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

    private static async Task<IResult> RemoveAvailabilitySlots(RemoveAvailabilitySlotsDTO dto, IRemoveAvailabilitySlotsUseCase removeAvailabilitySlotsUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await removeAvailabilitySlotsUseCase.ExecuteAsync(dto);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }
}
