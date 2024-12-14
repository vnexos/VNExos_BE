using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace VNExos.API.Controllers;

[Route("/")]
[ApiController]
public class HomeController : Controller
{
    [SwaggerIgnore]
    public IActionResult Index()
    {
        return Ok("{\"message\": \"Hello World\"}");
    }
}
