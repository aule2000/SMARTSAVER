using Microsoft.AspNetCore.Mvc;

namespace SmartSaver.Server.Controllers
{
    /// <summary>
    /// Does nothing. I don't understand why it is here.
    /// </summary>
    [ApiExplorerSettings(GroupName = "client")]
    [Produces("application/json")]
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public NoContentResult Get()
        {
            return NoContent();
        }
    }
}
