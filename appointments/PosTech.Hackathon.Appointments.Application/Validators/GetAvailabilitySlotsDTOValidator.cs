using FluentValidation;

namespace PosTech.Hackathon.Appointments.Application.Validators;

public class GetAvailabilitySlotsDTOValidator : AbstractValidator<Guid>
{
    public GetAvailabilitySlotsDTOValidator()
    {
        RuleFor(dto => dto)
        .NotEmpty()
        .WithMessage("Speciality is required.");
    }
}
