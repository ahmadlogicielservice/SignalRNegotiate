using Microsoft.AspNetCore.Mvc;

namespace SignalRNegotiate.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
