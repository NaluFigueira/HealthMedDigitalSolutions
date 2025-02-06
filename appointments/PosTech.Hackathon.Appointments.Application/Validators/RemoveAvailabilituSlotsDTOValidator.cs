using FluentValidation;

using PosTech.Hackathon.Appointments.Application.DTOs;

namespace PosTech.Hackathon.Appointments.Application.Validators;

public class RemoveAvailabilitySlotsDTOValidator : AbstractValidator<RemoveAvailabilitySlotsDTO>
{
    public RemoveAvailabilitySlotsDTOValidator()
    {
        RuleFor(dto => dto.DoctorId)
            .NotEmpty()
            .WithMessage("DoctorId is required.");

        RuleFor(dto => dto.SlotId)
            .NotEmpty()
            .WithMessage("SlotId is required.");
    }
}
