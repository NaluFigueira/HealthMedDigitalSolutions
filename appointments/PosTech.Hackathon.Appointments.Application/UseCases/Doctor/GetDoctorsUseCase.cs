using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentResults;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Application.UseCases.Patient;
using PosTech.Hackathon.Appointments.Application.Validators;
using PosTech.Hackathon.Appointments.Infra.Context;

namespace PosTech.Hackathon.Appointments.Application.UseCases.Doctor;
public class GetDoctorsUseCase(AppointmentsDBContext context, ILogger<GetDoctorsUseCase> logger) : IGetDoctorsUseCase
{
    private readonly AppointmentsDBContext _context = context;
    private readonly ILogger<GetDoctorsUseCase> _logger = logger;

    public async Task<Result<List<GetDoctorsDTO>>> ExecuteAsync(string speciality)
    {
        var validationResult = new GETDoctorsDTOValidator().Validate(speciality);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            LogErrors(errors);
            return Result.Fail(errors);
        }

        try
        {
            var doctors = await _context.Doctors
                .Where(a => a.Specialty == speciality)
                .ToListAsync();

            var doctorsDTOs = doctors
                .Select(a => new GetDoctorsDTO
                {
                    //Id = a.Id,
                    Name = a.Name,
                    CRM = a.CRM

                })
                .ToList();

            return Result.Ok(doctorsDTOs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving doctors.");
            return Result.Fail("An error occurred while retrieving doctors.");
        }
    }

    private void LogErrors(IEnumerable<string> errors)
    {
        foreach (var error in errors)
        {
            _logger.LogError("[ERR] GetAppointments: {error}", error);
        }
    }
}

