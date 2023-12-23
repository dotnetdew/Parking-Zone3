using Microsoft.AspNetCore.Mvc;
using Parking_Zone.Models;
using System.Diagnostics;
using System.Text.Encodings.Web;

namespace Parking_Zone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string Welcome(string name, int Id = 1)
        {
            return HtmlEncoder.Default.Encode($"Name: {name}, Id = {Id}");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
