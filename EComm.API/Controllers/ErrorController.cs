using Microsoft.AspNetCore.Mvc;

namespace EComm.API.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/exception")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Exception()
        {
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXx");
            return Problem();
        }
    }
}
