using FluentAssertions;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Validators;

namespace PosTech.Hackathon.Appointments.Tests.Unit.Validators;

public class AddAvailabilitySlotsDTOValidatorTest
{
    private readonly AddAvailabilitySlotsDTOValidator _validator;

    public AddAvailabilitySlotsDTOValidatorTest()
    {
        _validator = new AddAvailabilitySlotsDTOValidator();
    }

    [Fact]
    public void Validate_WhenDoctorIdIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new AddAvailabilitySlotsDTO
        {
            DoctorId = Guid.Empty,
            AvailabilitySlots = new List<AvailabilitySlotDTO> { new AvailabilitySlotDTO { Slot = DateTime.Now } }
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("DoctorId is required.");
    }

    [Fact]
    public void Validate_WhenAvailabilitySlotsIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new AddAvailabilitySlotsDTO
        {
            DoctorId = Guid.NewGuid(),
            AvailabilitySlots = new List<AvailabilitySlotDTO>()
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("AvailabilitySlots are required.");
    }

    [Fact]
    public void Validate_WhenAvailabilitySlotsHaveOverlappingSlots_ShouldReturnError()
    {
        // Arrange
        var dto = new AddAvailabilitySlotsDTO
        {
            DoctorId = Guid.NewGuid(),
            AvailabilitySlots = new List<AvailabilitySlotDTO>
            {
                new AvailabilitySlotDTO { Slot = DateTime.Now },
                new AvailabilitySlotDTO { Slot = DateTime.Now.AddMinutes(30) }
            }
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("Availability slots must be at least 1 hour apart.");
    }

    [Fact]
    public void Validate_WhenAvailabilitySlotsHaveInvalidTimeSlots_ShouldReturnError()
    {
        // Arrange
        var dto = new AddAvailabilitySlotsDTO
        {
            DoctorId = Guid.NewGuid(),
            AvailabilitySlots = new List<AvailabilitySlotDTO>
            {
                new AvailabilitySlotDTO { Slot = DateTime.Now.AddMinutes(15) }
            }
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("Availability slots must be on the hour or half-hour (e.g., 13:00, 13:30).");
    }
}
