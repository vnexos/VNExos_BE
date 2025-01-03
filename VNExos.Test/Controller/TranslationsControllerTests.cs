using MediatR;
using Microsoft.AspNetCore.Mvc;
using VNExos.API.Controllers;
using VNExos.API.Helpers;
using VNExos.Application.Translations.Commands.CreateTranslationsByLanguageCode;
using VNExos.Application.Translations.Commands.CreateTranslationsByOrigin;
using VNExos.Application.Translations.Commands.DeleteTranslation;
using VNExos.Application.Translations.Commands.UpdateTranslationByCodeAndOrigin;
using VNExos.Application.Translations.Commands.UpdateTranslationById;
using VNExos.Domain.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace VNExos.Test.Controller;

// Test Translations controller
public class TranslationsControllerTests
{
    private readonly IMediator _mediator;
    private readonly TranslationsController _controller;
    public TranslationsControllerTests()
    {
        _mediator = A.Fake<IMediator>();
        _controller = new TranslationsController(_mediator);
    }

    [Fact]
    public async void CreateByLanguage_Should_ReturnOk()
    {
        // Arrange
        var code = "vi-vn";
        var command = new CreateTranslationsByLanguageCodeCommand
        {
            Translations = new Dictionary<string, string>
            {
                { "SUCCESS", "Thành công" },
                { "FAILED", "Thất bại" }
            }
        };
        var expectedResult = new List<TranslationDto>
        {
            new() { LanguageId = Guid.NewGuid(), Id = Guid.NewGuid(), Origin = "SUCCESS", Translate = "Thành công" },
            new() { LanguageId = Guid.NewGuid(), Id = Guid.NewGuid(), Origin = "FAILED", Translate = "Thất bại" }
        };
        A.CallTo(() => _mediator.Send(A<CreateTranslationsByLanguageCodeCommand>._, A<CancellationToken>._))
            .Returns(expectedResult);

        // Act
        var res = await _controller.CreateByLanguage(code, command);
        var resultObject = res as ObjectResult;

        // Assert
        res.Should().BeOfType<ApiResponse<object>>();
        resultObject?.StatusCode.Should().Be(200);
        A.CallTo(() => _mediator.Send(command, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void CreateByOrigin_Should_ReturnOk()
    {
        // Arrange
        var origin = "SUCCESS";
        var command = new CreateTranslationsByOriginCommand
        {
            Languages = new Dictionary<string, string>
            {
                { "vi-vn", "Thành công" },
                { "en-us", "Success" }
            }
        };
        var expectedResult = new List<TranslationDto>
        {
            new() { LanguageId = Guid.NewGuid(), Id = Guid.NewGuid(), Origin = "SUCCESS", Translate = "Thành công" },
            new() { LanguageId = Guid.NewGuid(), Id = Guid.NewGuid(), Origin = "SUCCESS", Translate = "Success" }
        };
        A.CallTo(() => _mediator.Send(A<CreateTranslationsByLanguageCodeCommand>._, A<CancellationToken>._))
            .Returns(expectedResult);

        // Act
        var result = await _controller.CreateByOrigin(origin, command);
        var resultObject = result as ObjectResult;

        // Assert
        result.Should().BeOfType<ApiResponse<object>>();
        resultObject?.StatusCode.Should().Be(200);
        A.CallTo(() => _mediator.Send(command, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Theory]
    [InlineData("SUCCESS", null)]
    [InlineData(null, "Thành công")]
    [InlineData("SUCCESS", "Thành công")]
    public async void UpdateTranslationByCodeAndOrigin_Should_ReturnOk(string? _origin, string? _translate)
    {
        // Arrange
        string code = "vi-vn";
        string origin = "SUCCESSES";

        var command = new UpdateTranslationByCodeAndOriginCommand
        {
            Origin = _origin,
            Translate = _translate,
        };
        var expectedResult = new TranslationDto() { LanguageId = Guid.NewGuid(), Id = Guid.NewGuid(), Origin = _origin, Translate = _translate };
        A.CallTo(() => _mediator.Send(A<UpdateTranslationByCodeAndOriginCommand>._, A<CancellationToken>._))
            .Returns(expectedResult);

        // Act
        var result = await _controller.UpdateTranslationByCodeAndOrigin(code, origin, command);
        var resultObject = result as ObjectResult;

        // Assert
        result.Should().BeOfType<ApiResponse<object>>();
        resultObject?.StatusCode.Should().Be(200);
        A.CallTo(() => _mediator.Send(command, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Theory]
    [InlineData("SUCCESS", null)]
    [InlineData(null, "Thành công")]
    [InlineData("SUCCESS", "Thành công")]
    public async void UpdateTranslationById_Should_ReturnOk(string? _origin, string? _translate)
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new UpdateTranslationByIdCommand
        {
            Origin = _origin,
            Translate = _translate,
        };
        var expectedResult = new TranslationDto() { Id = id, Origin = _origin, Translate = _translate };
        A.CallTo(() => _mediator.Send(A<UpdateTranslationByIdCommand>._, A<CancellationToken>._))
            .Returns(expectedResult);

        // Act
        var result = await _controller.UpdateTranslationById(id, command);
        var resultObject = result as ObjectResult;

        // Assert
        result.Should().BeOfType<ApiResponse<object>>();
        resultObject?.StatusCode.Should().Be(200);
        A.CallTo(() => _mediator.Send(command, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void DeleteTranslation_Should_ReturnOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedResult = new TranslationDto() { Id = id, LanguageId = Guid.NewGuid(), Origin = "SUCCESS", Translate = "Thành công" };
        A.CallTo(() => _mediator.Send(A<DeleteTranslationCommand>._, A<CancellationToken>._))
            .Returns(expectedResult);

        // Act
        var result = await _controller.DeleteTranslation(id);
        var resultObject = result as ObjectResult;

        // Assert
        result.Should().BeOfType<ApiResponse<object>>();
        resultObject?.StatusCode.Should().Be(200);
        A.CallTo(() => _mediator.Send(A<DeleteTranslationCommand>._, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }
}
