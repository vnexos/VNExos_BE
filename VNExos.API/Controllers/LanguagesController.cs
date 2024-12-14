using MediatR;
using Microsoft.AspNetCore.Mvc;
using VNExos.Application.Languages.Queries.GetAllLanguages;

namespace VNExos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguagesController (IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllLanguagesQuery request)
    {
        var res = await mediator.Send(request);
        return Ok(res);
    }
}
