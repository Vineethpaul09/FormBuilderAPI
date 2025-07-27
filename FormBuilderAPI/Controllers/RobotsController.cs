using Microsoft.AspNetCore.Mvc;

namespace mcpwrapper.Controllers
{
    [ApiController]
    public class RobotsController : ControllerBase
    {
        [HttpGet("/robots.txt")]
        public IActionResult GetRobots()
        {
            return Content("User-agent: *\nDisallow:", "text/plain");
        }
    }
}
