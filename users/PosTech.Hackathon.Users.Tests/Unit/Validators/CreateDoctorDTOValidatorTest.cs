using System;
using FluentAssertions;
using PosTech.Hackathon.Users.Application.Validators;
using PosTech.Hackathon.Users.Tests.Builders;

namespace PosTech.Hackathon.Users.Tests.Unit;

public class CreateDoctorDTOValidatorTest
{
    private readonly CreateDoctorDTOValidator _validator;

    public CreateDoctorDTOValidatorTest()
    {
        _validator = new CreateDoctorDTOValidator();
    }

    [Fact]
    public void Validate_WhenUserNameIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreateDoctorDTOBuilder().WithUserName("").Build();

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
        var createUserDTO = new CreateDoctorDTOBuilder().WithName("").Build();

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
        var createUserDTO = new CreateDoctorDTOBuilder().WithCPF("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("CPF is required.");
    }

    [Fact]
    public void Validate_WhenCRMIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreateDoctorDTOBuilder().WithCRM("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("CRM is required.");
    }

    [Fact]
    public void Validate_WhenUserEmailIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreateDoctorDTOBuilder().WithEmail("").Build();

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
        var createUserDTO = new CreateDoctorDTOBuilder().WithPassword("").Build();

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
        var createUserDTO = new CreateDoctorDTOBuilder().WithRePassword("").Build();

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
        var createUserDTO = new CreateDoctorDTOBuilder().WithPassword("1").WithRePassword("2").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("RePassword and Password do not match.");
    }

    [Fact]
    public void Validate_WhenAppointmentValueIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreateDoctorDTOBuilder().WithAppointmentValue(0).Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("AppointmentValue is required.");
    }

    [Fact]
    public void Validate_WhenSpecialtyIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreateDoctorDTOBuilder().WithSpecialty("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("Specialty is required.");
    }

}
