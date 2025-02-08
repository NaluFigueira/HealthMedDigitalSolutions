using FluentValidation.TestHelper;

using PosTech.Hackathon.Appointments.Application.Validators;

namespace PosTech.Hackathon.Appointments.Tests.Unit.Validators;

public class GetAvailabilitySlotsDTOValidatorTests
{
    private readonly GetAvailabilitySlotsDTOValidator _validator;

    public GetAvailabilitySlotsDTOValidatorTests()
    {
        _validator = new GetAvailabilitySlotsDTOValidator();
    }

    [Fact]
    public void Validate_WhenGuidIsEmpty_ShouldReturnError()
    {
        // Arrange
        var guid = Guid.Empty;

        // Act
        var result = _validator.TestValidate(guid);

        // Assert
        result.ShouldHaveValidationErrorFor(g => g);
    }

    [Fact]
    public void Validate_WhenGuidIsNotEmpty_ShouldNotReturnError()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var result = _validator.TestValidate(guid);

        // Assert
        result.ShouldNotHaveValidationErrorFor(g => g);
    }

    [Fact]
    public void Validate_WhenGuidIsInvalid_ShouldReturnError()
    {
        // Arrange
        var guid = new Guid("00000000-0000-0000-0000-000000000000");

        // Act
        var result = _validator.TestValidate(guid);

        // Assert
        result.ShouldHaveValidationErrorFor(g => g);
    }

    [Fact]
    public void Validate_WhenGuidIsValid_ShouldNotReturnError()
    {
        // Arrange
        var guid = new Guid("12345678-1234-1234-1234-123456789012");

        // Act
        var result = _validator.TestValidate(guid);

        // Assert
        result.ShouldNotHaveValidationErrorFor(g => g);
    }
}
