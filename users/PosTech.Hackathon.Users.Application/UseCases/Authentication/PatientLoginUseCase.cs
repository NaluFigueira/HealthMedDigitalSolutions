using System;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PosTech.Hackathon.Users.Application.DTOs;
using PosTech.Hackathon.Users.Application.Interfaces.Services;
using PosTech.Hackathon.Users.Application.Interfaces.UseCases;
using PosTech.Hackathon.Users.Domain.Entities;

namespace PosTech.Hackathon.Users.Application.UseCases.Authentication;

public class PatientLoginUseCase(
    ILogger<IPatientLoginUseCase> logger,
    SignInManager<PatientUser> signInManager,
    ITokenService tokenService
) : IPatientLoginUseCase
{
    private readonly ILogger<IPatientLoginUseCase> _logger = logger;
    private readonly SignInManager<PatientUser> _signInManager = signInManager;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<string>> ExecuteAsync(PatientLoginDTO request)
    {
        var login = request.CPF.Length > 0 ? request.CPF : request.Email;
        var user = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user => (request.CPF.Length > 0 ? user.CPF : user.Email) == login.ToUpper());

        if (user == null)
        {
            var error = "User Not Found";
            _logger.LogError("[ERR] LogInUseCase: {error}", error);
            return Result.Fail([error]);
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);

        if (!result.Succeeded)
        {
            var error = "Password Authentication Failed";
            _logger.LogError("[ERR] LogInUseCase: {error}", error);
            return Result.Fail([error]);
        }

        var token = _tokenService.GenerateToken(user);
        return Result.Ok(token);
    }
}
