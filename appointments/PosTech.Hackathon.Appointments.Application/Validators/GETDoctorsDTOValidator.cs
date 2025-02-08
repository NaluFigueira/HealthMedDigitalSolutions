using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using PosTech.Hackathon.Appointments.Application.DTOs;

namespace PosTech.Hackathon.Appointments.Application.Validators;
public class GETDoctorsDTOValidator : AbstractValidator<string>
{
    public GETDoctorsDTOValidator()
    {
        RuleFor(dto => dto)
        .NotEmpty()
        .WithMessage("Speciality is required.");
    }

}
