using Microsoft.Extensions.Logging;

using Moq;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.UseCases.Patient;
using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Tests.Unit.UseCases;

public class GetAppointmentsUseCaseTests
{
    private readonly Mock<IAppointmentRepository> _mockRepository;
    private readonly Mock<ILogger<GetAppointmentsUseCase>> _mockLogger;
    private readonly GetAppointmentsUseCase _useCase;

    public GetAppointmentsUseCaseTests()
    {
        _mockRepository = new Mock<IAppointmentRepository>();
        _mockLogger = new Mock<ILogger<GetAppointmentsUseCase>>();
        _useCase = new GetAppointmentsUseCase(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenValidRequest()
    {
        // Arrange
        var request = new GetAppointmentsDTO
        {
            PatientId = Guid.NewGuid()
        };

        var appointments = new List<Appointment>
        {
            new Appointment { Id = Guid.NewGuid(), DoctorId = Guid.NewGuid(), PatientId = request.PatientId, SlotId = Guid.NewGuid(), Date = DateTime.Now }
        };

        _mockRepository.Setup(r => r.GetAppointmentsByPatientIdAsync(request.PatientId))
            .ReturnsAsync(appointments);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(appointments.Count, result.Value.Count);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenInvalidRequest()
    {
        // Arrange
        var request = new GetAppointmentsDTO
        {
            PatientId = Guid.Empty
        };

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenExceptionThrown()
    {
        // Arrange
        var request = new GetAppointmentsDTO
        {
            PatientId = Guid.NewGuid()
        };

        _mockRepository.Setup(r => r.GetAppointmentsByPatientIdAsync(request.PatientId))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
    }
}
