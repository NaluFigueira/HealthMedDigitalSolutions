using FluentValidation.TestHelper;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Validators;

namespace PosTech.Hackathon.Appointments.Tests.Unit.Validators;

public class RemoveAvailabilitySlotsDTOValidatorTest
{
    private readonly RemoveAvailabilitySlotsDTOValidator _validator;

    public RemoveAvailabilitySlotsDTOValidatorTest()
    {
        _validator = new RemoveAvailabilitySlotsDTOValidator();
    }

    [Fact]
    public void Validate_WhenDoctorIdIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new RemoveAvailabilitySlotsDTO
        {
            DoctorId = Guid.Empty,
            SlotId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.DoctorId)
              .WithErrorMessage("DoctorId is required.");
    }

    [Fact]
    public void Validate_WhenSlotIdIsEmpty_ShouldReturnError()
    {
        // Arrange
        var dto = new RemoveAvailabilitySlotsDTO
        {
            DoctorId = Guid.NewGuid(),
            SlotId = Guid.Empty
        };

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SlotId)
              .WithErrorMessage("SlotId is required.");
    }

    [Fact]
    public void Validate_WhenAllFieldsAreValid_ShouldNotReturnError()
    {
        // Arrange
        var dto = new RemoveAvailabilitySlotsDTO
        {
            DoctorId = Guid.NewGuid(),
            SlotId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
