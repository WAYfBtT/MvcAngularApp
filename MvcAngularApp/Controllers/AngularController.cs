using Microsoft.AspNetCore.Mvc;

namespace MvcAngularApp.Controllers
{
    public class AngularController : Controller
    {
        [HttpGet("/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
