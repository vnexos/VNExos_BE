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
public class LanguagesController (IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllLanguagesQuery request)
    {
        try
        {
            var res = await mediator.Send(request);
            return ApiResponse<ICollection<LanguageDto>>.CreateOk(res);
        } catch (Exception ex)
        {
            return ApiResponse<string>.CreateInternalError(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLanguageCommand request)
    {
        try
        {
            var res = await mediator.Send(request);
            return ApiResponse<LanguageDto>.CreateOk(res);
        } catch(Exception ex)
        {
            return ApiResponse<string>.CreateInternalError(ex.Message);
        }
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] UpdateLanguageCommand request)
    {
        request.Id = id;
        try
        {
            var res = await mediator.Send(request);
            return ApiResponse<LanguageDto>.CreateOk(res);
        } catch(Exception ex)
        {
            return ApiResponse<string>.CreateInternalError(ex.Message);
        }
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode([FromRoute] string code)
    {
        var request = new GetLanguageByCodeQuery { Code = code };
        try
        {
            var res = await mediator.Send(request);
            return ApiResponse<LanguageDto>.CreateOk(res);
        } catch (Exception ex)
        {
            return ApiResponse<string>.CreateInternalError(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var request = new DeleteLanguageCommand { Id = id };
        try
        {
            var res = await mediator.Send(request);
            return ApiResponse<LanguageDto>.CreateOk(res);
        } catch(Exception ex)
        {
            return ApiResponse<string>.CreateInternalError(ex.Message);
        }
    }
}
