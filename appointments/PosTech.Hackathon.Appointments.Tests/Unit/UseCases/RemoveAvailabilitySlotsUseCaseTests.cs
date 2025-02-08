using Microsoft.Extensions.Logging;

using Moq;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.UseCases.AvailabilitySlots;
using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Tests.Unit.UseCases;

public class RemoveAvailabilitySlotsUseCaseTests
{
    private readonly Mock<IAvailabilitySlotRepository> _mockRepository;
    private readonly Mock<ILogger<RemoveAvailabilitySlotsUseCase>> _mockLogger;
    private readonly RemoveAvailabilitySlotsUseCase _useCase;

    public RemoveAvailabilitySlotsUseCaseTests()
    {
        _mockRepository = new Mock<IAvailabilitySlotRepository>();
        _mockLogger = new Mock<ILogger<RemoveAvailabilitySlotsUseCase>>();
        _useCase = new RemoveAvailabilitySlotsUseCase(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenValidRequest()
    {
        // Arrange
        var request = new RemoveAvailabilitySlotsDTO
        {
            DoctorId = Guid.NewGuid(),
            SlotId = Guid.NewGuid()
        };

        var slot = new AvailabilitySlot
        {
            Id = request.SlotId,
            DoctorId = request.DoctorId,
            Slot = DateTime.Now,
            IsAvailable = true
        };

        _mockRepository.Setup(r => r.GetAvailabilitySlotAsync(request.SlotId, request.DoctorId))
            .ReturnsAsync(slot);
        _mockRepository.Setup(r => r.RemoveAvailabilitySlot(slot));
        _mockRepository.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenInvalidRequest()
    {
        // Arrange
        var request = new RemoveAvailabilitySlotsDTO
        {
            DoctorId = Guid.Empty,
            SlotId = Guid.Empty
        };

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenSlotNotFound()
    {
        // Arrange
        var request = new RemoveAvailabilitySlotsDTO
        {
            DoctorId = Guid.NewGuid(),
            SlotId = Guid.NewGuid()
        };

        _mockRepository.Setup(r => r.GetAvailabilitySlotAsync(request.SlotId, request.DoctorId))
            .ReturnsAsync((AvailabilitySlot)null!);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
    }
}
