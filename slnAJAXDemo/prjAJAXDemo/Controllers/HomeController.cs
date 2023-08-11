using Microsoft.AspNetCore.Mvc;
using prjAJAXDemo.Models;
using System.Diagnostics;

namespace prjAJAXDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult First() 
        {
        return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult register()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }

        public IActionResult Promise()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }


        public IActionResult jQuery()
        {
            return View();
        }

        public IActionResult Fetch()
        {
            return View();
        }

        public IActionResult MyPartial1()
        {
            return PartialView();
        }

        public IActionResult MyPartial2()
        {
            ViewBag.message = "來自partial2的內容";
            return PartialView();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}