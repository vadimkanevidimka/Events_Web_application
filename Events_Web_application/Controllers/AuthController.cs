using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    [Route("api/{controller}")]
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
