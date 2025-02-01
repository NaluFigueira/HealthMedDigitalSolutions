using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using PosTech.Hackathon.Users.Application.UseCases.Doctor;
using PosTech.Hackathon.Users.Domain.Entities;
using PosTech.Hackathon.Users.Infra.Interfaces;
using PosTech.Hackathon.Users.Infra.Queues;
using PosTech.Hackathon.Users.Tests.Builders;

namespace PosTech.Hackathon.Users.Tests.Unit;

public class CreateDoctorUseCaseTest
{
    private readonly Mock<UserManager<User>> _mockUserManager = new(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    private readonly Mock<IProducer> _mockProducer = new();
    private readonly Mock<ILogger<CreateDoctorUseCase>> _mockLogger = new();

    [Fact]
    public async Task ExecuteAsync_WhenValidRequest_ShouldReturnOk()
    {
        // Arrange
        var request = new CreateDoctorDTOBuilder().Build();
        var expectedUser = new User
        {
            UserName = request.UserName,
            Email = request.Email,
        };
        var user = new UserBuilder().WithEmail(request.Email).Build();

        _mockUserManager
            .Setup(repo => repo.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        _mockUserManager
            .Setup(repo => repo.Users)
            .Returns(new List<User>() { user }.AsQueryable());

        var useCase = new CreateDoctorUseCase(_mockLogger.Object, _mockProducer.Object, _mockUserManager.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _mockUserManager.Verify(repo => repo.CreateAsync(It.Is<User>(c =>
            c.Email == request.Email &&
            c.UserName == request.UserName
        ), It.Is<string>(s => s == request.Password)), Times.Once());

        _mockProducer.Verify(p => p.PublishMessageOnQueue(It.Is<Doctor>(d =>
            d.UserId == user.Id &&
            d.Name == request.Name &&
            d.Email == request.Email &&
            d.CPF == request.CPF &&
            d.CRM == request.CRM
        ), It.Is<string>(s => s == UserQueues.DoctorCreated)), Times.Once());
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserHasInvalidFields_ShouldReturnResultFail()
    {
        // Arrange
        var request = new CreateDoctorDTOBuilder().WithPassword("1").WithRePassword("2").Build();
        var useCase = new CreateDoctorUseCase(_mockLogger.Object, _mockProducer.Object, _mockUserManager.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();

        _mockUserManager.Verify(repo => repo.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never());
        _mockProducer.Verify(p => p.PublishMessageOnQueue(It.IsAny<Doctor>(), It.IsAny<string>()), Times.Never());
    }

    [Fact]
    public async Task ExecuteAsync_WhenDatabaseInsertionFails_ShouldReturnResultFail()
    {
        // Arrange
        var request = new CreateDoctorDTOBuilder().Build();

        _mockUserManager
            .Setup(repo => repo.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed([new IdentityError()]));

        var useCase = new CreateDoctorUseCase(_mockLogger.Object, _mockProducer.Object, _mockUserManager.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();

        _mockUserManager.Verify(repo => repo.CreateAsync(It.Is<User>(c =>
            c.Email == request.Email &&
            c.UserName == request.UserName
        ), It.Is<string>(s => s == request.Password)), Times.Once());
        _mockProducer.Verify(p => p.PublishMessageOnQueue(It.IsAny<Doctor>(), It.IsAny<string>()), Times.Never());
    }
}
