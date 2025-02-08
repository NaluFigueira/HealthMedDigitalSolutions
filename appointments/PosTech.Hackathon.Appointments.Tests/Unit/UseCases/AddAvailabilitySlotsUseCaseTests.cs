using Microsoft.Extensions.Logging;

using Moq;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.UseCases.AvailabilitySlots;
using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Tests.Unit.UseCases;

public class AddAvailabilitySlotsUseCaseTests
{
    private readonly Mock<IAvailabilitySlotRepository> _mockRepository;
    private readonly Mock<ILogger<AddAvailabilitySlotsUseCase>> _mockLogger;
    private readonly AddAvailabilitySlotsUseCase _useCase;

    public AddAvailabilitySlotsUseCaseTests()
    {
        _mockRepository = new Mock<IAvailabilitySlotRepository>();
        _mockLogger = new Mock<ILogger<AddAvailabilitySlotsUseCase>>();
        _useCase = new AddAvailabilitySlotsUseCase(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenValidRequest()
    {
        // Arrange
        var request = new AddAvailabilitySlotsDTO
        {
            DoctorId = Guid.NewGuid(),
            AvailabilitySlots = new List<AvailabilitySlotDTO>
            {
                new AvailabilitySlotDTO { Slot = DateTime.Today.AddHours(12) }
            },
        };

        _mockRepository.Setup(r => r.GetExistingSlotsAsync(It.IsAny<List<DateTime>>()))
            .ReturnsAsync(new List<AvailabilitySlot>());
        _mockRepository.Setup(r => r.AddAvailabilitySlotsAsync(It.IsAny<List<AvailabilitySlot>>()))
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
        var request = new AddAvailabilitySlotsDTO
        {
            DoctorId = Guid.Empty,
            AvailabilitySlots = new List<AvailabilitySlotDTO>()
        };

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenSlotAlreadyExists()
    {
        // Arrange
        var request = new AddAvailabilitySlotsDTO
        {
            DoctorId = Guid.NewGuid(),
            AvailabilitySlots = new List<AvailabilitySlotDTO>
            {
                new AvailabilitySlotDTO { Slot = DateTime.Now.AddHours(1) }
            }
        };

        _mockRepository.Setup(r => r.GetExistingSlotsAsync(It.IsAny<List<DateTime>>()))
            .ReturnsAsync(new List<AvailabilitySlot> { new AvailabilitySlot { Id = Guid.NewGuid(), IsAvailable = true, DoctorId = request.DoctorId, Slot = DateTime.Now.AddHours(1) } });

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
    }
}
