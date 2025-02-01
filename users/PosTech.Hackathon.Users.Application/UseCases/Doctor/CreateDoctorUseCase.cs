using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PosTech.Hackathon.Users.Application.DTOs;
using PosTech.Hackathon.Users.Application.Interfaces.UseCases;
using PosTech.Hackathon.Users.Application.Validators;
using PosTech.Hackathon.Users.Domain.Entities;
using PosTech.Hackathon.Users.Infra.Interfaces;
using PosTech.Hackathon.Users.Infra.Queues;

namespace PosTech.Hackathon.Users.Application.UseCases.Doctor;

public class CreateDoctorUseCase(
    ILogger<CreateDoctorUseCase> logger,
    IProducer producer,
    UserManager<User> userManager
) : ICreateDoctorUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IProducer _producer = producer;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<Result> ExecuteAsync(CreateDoctorDTO request)
    {
        var validationResult = new CreateDoctorDTOValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            LogErrors(errors);
            return Result.Fail(errors);
        }

        var newUser = new User
        {
            Email = request.Email,
            UserName = request.UserName,
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (result.Succeeded == false)
        {
            var errors = result.Errors.Select(e => e.Description);
            LogErrors(errors);
            return Result.Fail(errors);
        }

        var user = _userManager.Users.FirstOrDefault(u => u.Email == request.Email);

        if (user is null)
        {
            string[] errors = ["Created user not found"];
            LogErrors(errors);
            return Result.Fail(errors);
        }

        var doctor = new Domain.Entities.Doctor
        {
            UserId = user.Id,
            Name = request.Name,
            Email = request.Email,
            CPF = request.CPF,
            CRM = request.CRM,
        };

        _producer.PublishMessageOnQueue(doctor, UserQueues.DoctorCreated);

        return Result.Ok();
    }

    private void LogErrors(IEnumerable<string> errors)
    {
        foreach (var error in errors)
        {
            _logger.LogError("[ERR] CreateDoctorUseCase: {error}", error);
        }
    }
}
