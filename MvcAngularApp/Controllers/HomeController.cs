using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcAngularApp.Models;

namespace MvcAngularApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHostingEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("About")]
        public IActionResult About()
        {
            var path = Path.Combine(_env.ContentRootPath, @"TxtFiles\" + "About.txt");
            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                ViewBag.About = sr.ReadToEnd();
                sr.Close();
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("About")]
        public IActionResult About(string text)
        {
            var path = Path.Combine(_env.ContentRootPath, @"TxtFiles\" + "About.txt");
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.WriteLine(text);
                sw.Close();
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
