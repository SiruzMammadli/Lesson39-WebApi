using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("/test")]
[ApiController]
public sealed class TestController : ControllerBase
{
    [HttpGet]
    public string GetHello()
    {
        return "Hello World";
    }
}