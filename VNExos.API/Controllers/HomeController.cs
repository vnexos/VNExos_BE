using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VNExos.API.Helpers;

namespace VNExos.API.Controllers;

[Route("/")]
[ApiController]
public class HomeController : ControllerBase
{
    [SwaggerIgnore]
    [HttpGet]
    public IActionResult Index()
    {
        return ApiResponse<string>.CreateOk("{\"message\": \"Hello World\"}");
    }
}
