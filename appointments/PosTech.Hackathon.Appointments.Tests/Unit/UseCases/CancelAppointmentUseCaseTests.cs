using Microsoft.Extensions.Logging;

using Moq;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.UseCases.Patient;
using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Tests.Unit.UseCases;

public class CancelAppointmentUseCaseTests
{
    private readonly Mock<IAppointmentRepository> _mockRepository;
    private readonly Mock<ILogger<CancelAppointmentUseCase>> _mockLogger;
    private readonly CancelAppointmentUseCase _useCase;

    public CancelAppointmentUseCaseTests()
    {
        _mockRepository = new Mock<IAppointmentRepository>();
        _mockLogger = new Mock<ILogger<CancelAppointmentUseCase>>();
        _useCase = new CancelAppointmentUseCase(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenValidRequest()
    {
        // Arrange
        var request = new CancelAppointmentDTO
        {
            AppointmentId = Guid.NewGuid(),
            PatientId = Guid.NewGuid(),
            CancellationReason = "Patient request"
        };

        var appointment = new Appointment
        {
            Id = request.AppointmentId,
            PatientId = request.PatientId,
            DoctorId = Guid.NewGuid(),
            SlotId = Guid.NewGuid(),
            Date = DateTime.Now
        };

        _mockRepository.Setup(r => r.GetAppointmentAsync(request.AppointmentId, request.PatientId))
            .ReturnsAsync(appointment);
        _mockRepository.Setup(r => r.RemoveAppointmentAsync(appointment))
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
        var request = new CancelAppointmentDTO
        {
            AppointmentId = Guid.Empty,
            PatientId = Guid.Empty,
            CancellationReason = string.Empty
        };

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenAppointmentNotFound()
    {
        // Arrange
        var request = new CancelAppointmentDTO
        {
            AppointmentId = Guid.NewGuid(),
            PatientId = Guid.NewGuid(),
            CancellationReason = "Patient request"
        };

        _mockRepository.Setup(r => r.GetAppointmentAsync(request.AppointmentId, request.PatientId))
            .ReturnsAsync((Appointment)null!);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
    }
}
