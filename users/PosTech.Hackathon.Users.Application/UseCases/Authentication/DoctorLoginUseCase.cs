using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PosTech.Hackathon.Users.Application.DTOs;
using PosTech.Hackathon.Users.Application.Interfaces.Services;
using PosTech.Hackathon.Users.Application.Interfaces.UseCases;
using PosTech.Hackathon.Users.Domain.Entities;

namespace PosTech.Hackathon.Users.Application.UseCases.Authentications;

public class DoctorLoginUseCase(
    ILogger<IDoctorLoginUseCase> logger,
    SignInManager<DoctorUser> signInManager,
    ITokenService tokenService
) : IDoctorLoginUseCase
{
    private readonly ILogger<IDoctorLoginUseCase> _logger = logger;
    private readonly SignInManager<DoctorUser> _signInManager = signInManager;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<string>> ExecuteAsync(DoctorLoginDTO request)
    {
        var user = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.CRM == request.CRM.ToUpper());

        if (user == null)
        {
            var error = "User Not Found";
            _logger.LogError("[ERR] LogInUseCase: {error}", error);
            return Result.Fail([error]);
        }

        var result = await _signInManager.PasswordSignInAsync(request.CRM, request.Password, false, false);

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
