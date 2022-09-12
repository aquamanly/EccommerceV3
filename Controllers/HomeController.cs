using EccommerceV3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EccommerceV3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult ProductsPartial() =>
            new PartialViewResult
            {
                ViewName = "_Products",
                ViewData = ViewData,
            };

        public IActionResult Index()
        {
            return View();
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