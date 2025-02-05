using FluentValidation;
using PosTech.Hackathon.Users.Application.DTOs;

namespace PosTech.Hackathon.Users.Application.Validators;

public class CreateDoctorDTOValidator : AbstractValidator<CreateDoctorDTO>
{
        public CreateDoctorDTOValidator()
        {
                RuleFor(user => user.UserName)
                        .NotEmpty()
                        .WithMessage("UserName is required.");

                RuleFor(user => user.Name)
                        .NotEmpty()
                        .WithMessage("Name is required.");

                RuleFor(user => user.Email)
                        .NotEmpty()
                        .WithMessage("Email is required.");

                RuleFor(user => user.CRM)
                        .NotEmpty()
                        .WithMessage("CRM is required.");

                RuleFor(user => user.CPF)
                        .NotEmpty()
                        .WithMessage("CPF is required.");

                RuleFor(user => user.AppointmentValue)
                        .GreaterThan(0)
                        .WithMessage("AppointmentValue is required.");

                RuleFor(user => user.Specialty)
                        .NotEmpty()
                        .WithMessage("Specialty is required.");

                RuleFor(user => user.Password)
                        .NotEmpty()
                        .WithMessage("Password is required.");

                RuleFor(user => user.RePassword)
                        .NotEmpty()
                        .WithMessage("RePassword is required.")
                        .Must((user, repassword) => user.Password == repassword)
                        .WithMessage("RePassword and Password do not match.");

        }
}
