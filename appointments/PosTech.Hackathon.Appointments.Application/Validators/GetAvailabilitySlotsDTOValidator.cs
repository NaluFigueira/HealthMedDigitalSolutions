using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace PosTech.Hackathon.Appointments.Application.Validators;
internal class GetAvailabilitySlotsDTOValidator : AbstractValidator<Guid>
{
    public GetAvailabilitySlotsDTOValidator()
    {
        RuleFor(dto => dto )
        .NotEmpty()
        .WithMessage("Speciality is required.");
    }
}