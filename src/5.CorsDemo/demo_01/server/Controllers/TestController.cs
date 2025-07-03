
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[Controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    // [EnableCors("test")]
    public IActionResult GetTest()
    {
        return Ok("Test successful");
    }

    [HttpGet("{id}")]
    public ActionResult GetCustomTest(long id)
    {
        Response.Headers.Append("Access-Control-Allow-Origin", "*");
        return Ok("this is a custom test");
    }
}