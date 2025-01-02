using MediatR;
using Microsoft.AspNetCore.Mvc;
using VNExos.API.Helpers;
using VNExos.Application.Translations.Commands.CreateTranslationsByLanguageCode;
using VNExos.Application.Translations.Commands.CreateTranslationsByOrigin;
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
    public async Task<IActionResult> CreateByLanguage([FromRoute] string origin, [FromBody] CreateTranslationsByOriginCommand request)
    {
        request.Origin = origin;
        return await Execute(request);
    }
}
