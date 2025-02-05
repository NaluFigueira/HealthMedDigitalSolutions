using System;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using PosTech.Hackathon.Users.Application.Interfaces.Services;
using PosTech.Hackathon.Users.Application.Interfaces.UseCases;
using PosTech.Hackathon.Users.Application.UseCases.Authentication;
using PosTech.Hackathon.Users.Domain.Entities;
using PosTech.Hackathon.Users.Tests.Builders;

namespace PosTech.Hackathon.Users.Tests.Unit;

public class PatientLoginUseCaseTest
{
    private Mock<SignInManager<PatientUser>> _mockSignManager;
    private readonly Mock<ITokenService> _mockTokenService;

    public PatientLoginUseCaseTest()
    {
        var mockUserManager = new Mock<UserManager<PatientUser>>(Mock.Of<IUserStore<PatientUser>>(), null, null, null, null, null, null, null, null);

        _mockSignManager = new Mock<SignInManager<PatientUser>>(mockUserManager.Object);

        _mockTokenService = new Mock<ITokenService>();
    }

    [Fact]
    public async Task ExecuteAsync_WhenValidRequest_ShouldReturnOk()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<IPatientLoginUseCase>>();

        var request = new PatientLoginDTOBuilder().Build();

        var user = new PatientUserBuilder().WithCPF(request.CPF).WithEmail(request.Email).Build();

        var mockUserManager = new Mock<UserManager<PatientUser>>(Mock.Of<IUserStore<PatientUser>>(), null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(m => m.Users)
            .Returns(new List<PatientUser> { user }.AsQueryable());

        _mockSignManager = new Mock<SignInManager<PatientUser>>(mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<PatientUser>>(), null, null, null);

        _mockSignManager
            .Setup(m => m.PasswordSignInAsync(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Success);

        _mockTokenService
            .Setup(m => m.GenerateToken(It.IsAny<PatientUser>()))
            .Returns("test");

        var useCase = new PatientLoginUseCase(mockLogger.Object, _mockSignManager.Object, _mockTokenService.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNullOrEmpty();

        _mockSignManager.Verify(repo => repo.PasswordSignInAsync(
            It.Is<string>(u => u == user.UserName),
            It.Is<string>(p => p == request.Password),
            It.Is<bool>(isPersistent => isPersistent == false),
            It.Is<bool>(lockoutOnFailure => lockoutOnFailure == false)
        ), Times.Once());

        _mockTokenService.Verify(repo => repo.GenerateToken(It.Is<PatientUser>(user => user.CPF == request.CPF)), Times.Once());
    }

    [Fact]
    public async Task ExecuteAsync_WhenManagersFails_ShouldReturnResultFail()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<PatientLoginUseCase>>();

        var request = new PatientLoginDTOBuilder().Build();

        var user = new PatientUserBuilder().WithCPF(request.CPF).WithEmail(request.Email).Build();

        var mockUserManager = new Mock<UserManager<PatientUser>>(Mock.Of<IUserStore<PatientUser>>(), null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(m => m.Users)
            .Returns(new List<PatientUser> { user }.AsQueryable());

        _mockSignManager = new Mock<SignInManager<PatientUser>>(mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<PatientUser>>(), null, null, null);

        _mockSignManager
            .Setup(m => m.PasswordSignInAsync(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Failed);

        var useCase = new PatientLoginUseCase(mockLogger.Object, _mockSignManager.Object, _mockTokenService.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();

        _mockSignManager.Verify(repo => repo.PasswordSignInAsync(
            It.Is<string>(u => u == user.UserName),
            It.Is<string>(p => p == request.Password),
            It.Is<bool>(isPersistent => isPersistent == false),
            It.Is<bool>(lockoutOnFailure => lockoutOnFailure == false)
        ), Times.Once());
        _mockTokenService.Verify(repo => repo.GenerateToken(It.Is<PatientUser>(user => user.CPF == request.CPF)), Times.Never());
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserNotFound_ShouldReturnResultFail()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<PatientLoginUseCase>>();

        var request = new PatientLoginDTOBuilder().Build();

        var mockUserManager = new Mock<UserManager<PatientUser>>(Mock.Of<IUserStore<PatientUser>>(), null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(m => m.Users)
            .Returns(new List<PatientUser>().AsQueryable());
        _mockSignManager = new Mock<SignInManager<PatientUser>>(mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<PatientUser>>(), null, null, null);

        var useCase = new PatientLoginUseCase(mockLogger.Object, _mockSignManager.Object, _mockTokenService.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();

        _mockSignManager.Verify(repo => repo.PasswordSignInAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<bool>(),
            It.IsAny<bool>()
        ), Times.Never());
        _mockTokenService.Verify(repo => repo.GenerateToken(It.IsAny<PatientUser>()), Times.Never());
    }
}
