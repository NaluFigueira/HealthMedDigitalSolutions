using FluentAssertions;
using PosTech.Hackathon.Users.Application.Validators;
using PosTech.Hackathon.Users.Tests.Builders;

namespace PosTech.Hackathon.Users.Tests.Unit;

public class CreatePatientDTOValidatorTest
{
    private readonly CreatePatientDTOValidator _validator;

    public CreatePatientDTOValidatorTest()
    {
        _validator = new CreatePatientDTOValidator();
    }

    [Fact]
    public void Validate_WhenUserNameIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreatePatientDTOBuilder().WithUserName("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("UserName is required.");
    }

    [Fact]
    public void Validate_WhenNameIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreatePatientDTOBuilder().WithName("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("Name is required.");
    }

    [Fact]
    public void Validate_WhenCPFIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreatePatientDTOBuilder().WithCPF("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("CPF is required.");
    }

    [Fact]
    public void Validate_WhenUserEmailIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreatePatientDTOBuilder().WithEmail("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("Email is required.");
    }

    [Fact]
    public void Validate_WhenUsePasswordIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreatePatientDTOBuilder().WithPassword("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("Password is required.");
    }

    [Fact]
    public void Validate_WhenUseRePasswordIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreatePatientDTOBuilder().WithRePassword("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("RePassword is required.");
    }

    [Fact]
    public void Validate_WhenUseRePasswordIsDifferentFromPassword_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreatePatientDTOBuilder().WithPassword("1").WithRePassword("2").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("RePassword and Password do not match.");
    }
}
