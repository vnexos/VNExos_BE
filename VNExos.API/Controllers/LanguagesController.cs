using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VNExos.API.Helpers;
using VNExos.Application.Languages.Commands.CreateLanguage;
using VNExos.Application.Languages.Commands.DeleteLanguage;
using VNExos.Application.Languages.Commands.UpdateLanguage;
using VNExos.Application.Languages.Queries.GetAllLanguages;
using VNExos.Application.Languages.Queries.GetLanguageByCode;
using VNExos.Domain.Dtos;

namespace VNExos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguagesController (IMediator mediator) : CommonController<LanguageDto>(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllLanguagesQuery request)
    {
        return await Execute(request);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLanguageCommand request)
    {
        return await Execute(request);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] UpdateLanguageCommand request)
    {
        return await Execute(request);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode([FromRoute] string code)
    {
        return await Execute(new GetLanguageByCodeQuery { Code = code });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return await Execute(new DeleteLanguageCommand { Id = id });
    }
}
