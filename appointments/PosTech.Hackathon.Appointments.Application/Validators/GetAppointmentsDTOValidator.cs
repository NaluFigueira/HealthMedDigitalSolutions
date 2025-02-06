using FluentValidation;

using PosTech.Hackathon.Appointments.Application.DTOs;

namespace PosTech.Hackathon.Appointments.Application.Validators;

public class GetAppointmentsDTOValidator : AbstractValidator<GetAppointmentsDTO>
{
    public GetAppointmentsDTOValidator()
    {
        RuleFor(dto => dto.PatientId)
            .NotEmpty()
            .WithMessage("PatientId is required.");
    }
}
