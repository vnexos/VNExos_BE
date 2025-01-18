using MediatR;
using Microsoft.AspNetCore.Mvc;
using VNExos.API.Helpers;
using VNExos.Application.Translations.Commands.CreateTranslationsByLanguageCode;
using VNExos.Application.Translations.Commands.CreateTranslationsByOrigin;
using VNExos.Application.Translations.Commands.DeleteTranslation;
using VNExos.Application.Translations.Commands.UpdateTranslationByCodeAndOrigin;
using VNExos.Application.Translations.Commands.UpdateTranslationById;
using VNExos.Domain.Dtos;

namespace VNExos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TranslationsController(IMediator mediator) : CommonController<TranslationDto>(mediator)
{
    [HttpPost("code/{code}")]
    public async Task<IActionResult> CreateByLanguage([FromRoute] string code, [FromBody] CreateTranslationsByLanguageCodeCommand request)
    {
        request.LanguageCode = code;
        return await Execute(request);
    }

    [HttpPost("origin/{origin}")]
    public async Task<IActionResult> CreateByOrigin([FromRoute] string origin, [FromBody] CreateTranslationsByOriginCommand request)
    {
        request.Origin = origin;
        return await Execute(request);
    }

    [HttpPatch("{code}/{origin}")]
    public async Task<IActionResult> UpdateTranslationByCodeAndOrigin([FromRoute] string code, [FromRoute] string origin, [FromBody] UpdateTranslationByCodeAndOriginCommand request)
    {
        request.LanguageCode = code;
        request.TranslationOrigin = origin;

        return await Execute(request);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateTranslationById([FromRoute] Guid id, [FromBody] UpdateTranslationByIdCommand request)
    {
        request.Id = id;
        return await Execute(request);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTranslation([FromRoute] Guid id)
    {
        return await Execute(new DeleteTranslationCommand { Id = id });
    }
}
