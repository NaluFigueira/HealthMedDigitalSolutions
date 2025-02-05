using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using PosTech.Hackathon.Users.Application.Interfaces.Services;
using PosTech.Hackathon.Users.Application.Interfaces.UseCases;
using PosTech.Hackathon.Users.Application.UseCases.Authentications;
using PosTech.Hackathon.Users.Domain.Entities;
using PosTech.Hackathon.Users.Tests.Builders;

namespace PosTech.Hackathon.Users.Tests.Unit;

public class DoctorLoginUseCaseTest
{
    private Mock<SignInManager<DoctorUser>> _mockSignManager;
    private readonly Mock<ITokenService> _mockTokenService;

    public DoctorLoginUseCaseTest()
    {
        var mockUserManager = new Mock<UserManager<DoctorUser>>(Mock.Of<IUserStore<DoctorUser>>(), null, null, null, null, null, null, null, null);

        _mockSignManager = new Mock<SignInManager<DoctorUser>>(mockUserManager.Object);

        _mockTokenService = new Mock<ITokenService>();
    }

    [Fact]
    public async Task ExecuteAsync_WhenValidRequest_ShouldReturnOk()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<IDoctorLoginUseCase>>();

        var request = new DoctorLoginDTOBuilder().Build();

        var user = new DoctorUserBuilder().WithCRM(request.CRM).Build();

        var mockUserManager = new Mock<UserManager<DoctorUser>>(Mock.Of<IUserStore<DoctorUser>>(), null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(m => m.Users)
            .Returns(new List<DoctorUser> { user }.AsQueryable());

        _mockSignManager = new Mock<SignInManager<DoctorUser>>(mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<DoctorUser>>(), null, null, null);

        _mockSignManager
            .Setup(m => m.PasswordSignInAsync(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Success);

        _mockTokenService
            .Setup(m => m.GenerateToken(It.IsAny<DoctorUser>()))
            .Returns("test");

        var useCase = new DoctorLoginUseCase(mockLogger.Object, _mockSignManager.Object, _mockTokenService.Object);

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

        _mockTokenService.Verify(repo => repo.GenerateToken(It.Is<DoctorUser>(user => user.CRM == request.CRM)), Times.Once());
    }

    [Fact]
    public async Task ExecuteAsync_WhenManagersFails_ShouldReturnResultFail()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<DoctorLoginUseCase>>();

        var request = new DoctorLoginDTOBuilder().Build();

        var user = new DoctorUserBuilder().WithCRM(request.CRM).Build();

        var mockUserManager = new Mock<UserManager<DoctorUser>>(Mock.Of<IUserStore<DoctorUser>>(), null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(m => m.Users)
            .Returns(new List<DoctorUser> { user }.AsQueryable());

        _mockSignManager = new Mock<SignInManager<DoctorUser>>(mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<DoctorUser>>(), null, null, null);

        _mockSignManager
            .Setup(m => m.PasswordSignInAsync(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Failed);

        var useCase = new DoctorLoginUseCase(mockLogger.Object, _mockSignManager.Object, _mockTokenService.Object);

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
        _mockTokenService.Verify(repo => repo.GenerateToken(It.Is<DoctorUser>(user => user.CRM == request.CRM)), Times.Never());
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserNotFound_ShouldReturnResultFail()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<DoctorLoginUseCase>>();

        var request = new DoctorLoginDTOBuilder().Build();

        var mockUserManager = new Mock<UserManager<DoctorUser>>(Mock.Of<IUserStore<DoctorUser>>(), null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(m => m.Users)
            .Returns(new List<DoctorUser>().AsQueryable());
        _mockSignManager = new Mock<SignInManager<DoctorUser>>(mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<DoctorUser>>(), null, null, null);

        var useCase = new DoctorLoginUseCase(mockLogger.Object, _mockSignManager.Object, _mockTokenService.Object);

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
        _mockTokenService.Verify(repo => repo.GenerateToken(It.IsAny<DoctorUser>()), Times.Never());
    }
}
