using FluentValidation;

using PosTech.Hackathon.Appointments.Application.DTOs;

namespace PosTech.Hackathon.Appointments.Application.Validators;

public class CancelAppointmentDTOValidator : AbstractValidator<CancelAppointmentDTO>
{
    public CancelAppointmentDTOValidator()
    {
        RuleFor(dto => dto.PatientId)
            .NotEmpty()
            .WithMessage("PatientId is required.");

        RuleFor(dto => dto.AppointmentId)
            .NotEmpty()
            .WithMessage("AppointmentId is required.");

        RuleFor(dto => dto.CancellationReason)
            .NotEmpty()
            .WithMessage("CancellationReason is required.")
            .MaximumLength(500)
            .WithMessage("CancellationReason must not exceed 500 characters.");
    }
}
