using MediatR;
using Microsoft.AspNetCore.Mvc;
using VNExos.API.Controllers;
using VNExos.API.Helpers;
using VNExos.Application.Languages.Commands.CreateLanguage;
using VNExos.Application.Languages.Commands.DeleteLanguage;
using VNExos.Application.Languages.Commands.UpdateLanguage;
using VNExos.Application.Languages.Queries.GetAllLanguages;
using VNExos.Application.Languages.Queries.GetLanguageByCode;
using VNExos.Domain.Dtos;

namespace VNExos.Test.Controller;

// Test Languages controller
public class LanguagesControllerTests
{
    private readonly IMediator _mediator;
    private readonly LanguagesController _controller;
    public LanguagesControllerTests() 
    {
        _mediator = A.Fake<IMediator>();
        _controller = new LanguagesController(_mediator);
    }

    [Fact]
    public async void GetAll_Should_ReturnOk()
    {
        // Arrange
        var languages = new List<LanguageDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Code = "vi-vn",
                Name = "Tiếng Việt",
                FlagUrl = "flags/vi-vn.png",
                IsDefault = true,
                RightToLeft = false,
                Description = "LANGUAGE_VIETNAMESE",
            },
            new()
            {
                Id = Guid.NewGuid(),
                Code = "en-us",
                Name = "English (United States)",
                FlagUrl = "flags/en-us.png",
                IsDefault = false,
                RightToLeft = false,
                Description = "LANGUAGE_ENGLISH_US",
            },
        };
        A.CallTo(() => _mediator.Send(A<GetAllLanguagesQuery>._, A<CancellationToken>._))
            .Returns(languages);

        // Act
        var result = await _controller.GetAll(new GetAllLanguagesQuery());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ApiResponse<object>>();

        var objectResponce = result as ObjectResult;
        objectResponce?.StatusCode.Should().Be(200);
    }

    [Fact]
    public async void GetByCode_Should_ReturnOk()
    {
        // Arrange
        var languageCode = "vi-vn";
        var expectLanguage = new LanguageDto
        {
            Code = "vi-vn",
            Name = "Tiếng Việt",
            FlagUrl = "flags/vi-vn.png",
            IsDefault = true,
            RightToLeft = false,
            Description = "LANGUAGE_VIETNAMESE",
        };
        A.CallTo(() => _mediator.Send(A<GetLanguageByCodeQuery>._, A<CancellationToken>._))
            .Returns(expectLanguage);

        // Act
        var result = await _controller.GetByCode(languageCode);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ApiResponse<object>>();

        var objectResponce = result as ObjectResult;
        objectResponce?.StatusCode.Should().Be(200);
        A.CallTo(() => _mediator.Send(A<GetLanguageByCodeQuery>._, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void Create_WithValidData_Should_ReturnOk()
    {
        // Arrange
        var command = new CreateLanguageCommand
        {
            Code = "en-uk",
            Name = "English (United Kingdom)",

            FlagUrl = "https://vnexos.com/flags/en.png",

            IsDefault = false,
            RightToLeft = false,
        };
        var validator = new CreateLanguageCommandValidator();
        var validationResult = await validator.ValidateAsync(command);
        var expectResult = new LanguageDto
        { 
            Id = Guid.NewGuid(),
            Code = command.Code,
            Name = command.Name,

            FlagUrl = command.FlagUrl,

            IsDefault = command.IsDefault,
            RightToLeft = command.RightToLeft,
        };

        A.CallTo(() => _mediator.Send(command, A<CancellationToken>._))
            .Returns(expectResult);

        // Act
        var result = await _controller.Create(command);
        var apiResponse = result as ObjectResult;

        // Assert
        result.Should().BeOfType<ApiResponse<object>>();
        validationResult.IsValid.Should().BeTrue();
        apiResponse?.StatusCode.Should().Be(200);
    }

    [Theory]
    [InlineData("", "English (India)", "https://vnexos.com/flags/en-in.png", "LANGUAGE_CODE_EMPTY")]
    [InlineData("en", "English (India)", "https://vnexos.com/flags/en-in.png", "LANGUAGE_CODE_WRONG_FORMAT")]
    [InlineData("en-in", "", "https://vnexos.com/flags/en-in.png", "LANGUAGE_NAME_EMPTY")]
    [InlineData("en-in", "English (India)", "", "LANGUAGE_FLAG_EMPTY")]
    [InlineData("en-in", "English (India)", "invalid-url", "LANGUAGE_FLAG_NOT_VALID")]
    public async void Create_WithInvalidData_Should_ReturnValidationError(string code, string name, string flagUrl, string expectedError)
    {
        // Arrange
        var command = new CreateLanguageCommand
        {
            Code = code,
            Name = name,
            FlagUrl = flagUrl
        };
        var validator = new CreateLanguageCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        // Act
        var result = await _controller.Create(command);
        var objectResult = result as BadRequestObjectResult;

        // Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(x => x.ErrorMessage == expectedError);
    }

    [Fact]
    public async void Update_WithValidData_Should_ReturnOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new UpdateLanguageCommand
        {
            Code = "en-uk",
            Name = "English (United Kingdom)",
            FlagUrl = "https://vnexos.com/flags/en.png",
        };
        var validator = new UpdateLanguageCommandValidator();
        var validationResult = await validator.ValidateAsync(command);
        var expectedResult = new LanguageDto
        {
            Id = id,
            Code = command.Code,
            Name = command.Name,
            FlagUrl = command.FlagUrl,
        };
        A.CallTo(() => _mediator.Send(command, A<CancellationToken>._))
            .Returns(expectedResult);

        // Act
        var result = await _controller.Patch(id, command);
        var apiResponse = result as ObjectResult;

        // Assert
        result.Should().BeOfType<ApiResponse<object>>();
        validationResult.IsValid.Should().BeTrue();
        apiResponse?.StatusCode.Should().Be(200);
    }

    [Theory]
    [InlineData("061d8cf7-f08b-423b-8621-80c53f200727", "en", "English (UK)", "https://vnexos.com/flags/en-in.png", "LANGUAGE_CODE_WRONG_FORMAT")]
    [InlineData("061d8cf7-f08b-423b-8621-80c53f200727", "en-uk", "English (UK)", "//vnexos.com/flags/en-in.png", "LANGUAGE_FLAG_NOT_VALID")]
    public async void Update_WithInvalidData_Should_ReturnValidationError(Guid id, string code, string name, string flagUrl, string expectedError)
    {
        // Arrange
        var command = new CreateLanguageCommand
        {
            Id = id,
            Code = code,
            Name = name,
            FlagUrl = flagUrl
        };
        var validator = new CreateLanguageCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        // Act
        var result = await _controller.Create(command);
        var objectResult = result as BadRequestObjectResult;

        // Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(x => x.ErrorMessage == expectedError);
    }

    [Fact]
    public async void Delete_Should_ReturnOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedResult = A.Fake<LanguageDto>();
        A.CallTo(() => _mediator.Send(A<DeleteLanguageCommand>._, A<CancellationToken>._))
            .Returns(expectedResult);

        // Act
        var result = await _controller.Delete(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ApiResponse<object>>();

        var objectResponce = result as ObjectResult;
        objectResponce?.StatusCode.Should().Be(200);
        A.CallTo(() => _mediator.Send(A<DeleteLanguageCommand>._, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }
}
