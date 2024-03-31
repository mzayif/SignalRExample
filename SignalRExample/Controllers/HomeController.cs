using Microsoft.AspNetCore.Mvc;
using SignalRExample.Services;

namespace SignalRExample.Controllers;
[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly SignalRService _service;

    public HomeController(SignalRService service)
    {
        this._service = service;
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        await _service.SendMessageAsync("Merhaba");
        return Ok();
    }
}
