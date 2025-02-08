using FluentAssertions;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Validators;

namespace PosTech.Hackathon.Appointments.Tests.Unit.Validators;

public class CancelAppointmentDTOValidatorTest
{
    private readonly CancelAppointmentDTOValidator _validator;

    public CancelAppointmentDTOValidatorTest()
    {
        _validator = new CancelAppointmentDTOValidator();
    }

    [Fact]
    public void Validate_WhenPatientIdIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new CancelAppointmentDTO
        {
            PatientId = Guid.Empty,
            AppointmentId = Guid.NewGuid(),
            CancellationReason = "Some reason"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("PatientId is required.");
    }

    [Fact]
    public void Validate_WhenAppointmentIdIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new CancelAppointmentDTO
        {
            PatientId = Guid.NewGuid(),
            AppointmentId = Guid.Empty,
            CancellationReason = "Some reason"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("AppointmentId is required.");
    }

    [Fact]
    public void Validate_WhenCancellationReasonIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new CancelAppointmentDTO
        {
            PatientId = Guid.NewGuid(),
            AppointmentId = Guid.NewGuid(),
            CancellationReason = string.Empty
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("CancellationReason is required.");
    }

    [Fact]
    public void Validate_WhenCancellationReasonExceedsMaxLength_ShouldReturnError()
    {
        // Arrange
        var dto = new CancelAppointmentDTO
        {
            PatientId = Guid.NewGuid(),
            AppointmentId = Guid.NewGuid(),
            CancellationReason = new string('a', 501)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("CancellationReason must not exceed 500 characters.");
    }
}
