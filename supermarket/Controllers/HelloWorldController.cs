using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet("hello")]
    [Authorize]  // Yêu cầu token hợp lệ
    public IActionResult GetHello()
    {
        return Ok("Hello World");
    }
}
