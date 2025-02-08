using FluentValidation;

namespace PosTech.Hackathon.Appointments.Application.Validators;

public class GetDoctorsDTOValidator : AbstractValidator<string>
{
    public GetDoctorsDTOValidator()
    {
        RuleFor(dto => dto)
        .NotEmpty()
        .WithMessage("Speciality is required.");
    }
}
