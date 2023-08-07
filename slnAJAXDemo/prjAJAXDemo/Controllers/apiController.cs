using Microsoft.AspNetCore.Mvc;

namespace prjAJAXDemo.Controllers
{
    public class apiController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello AJAX"); 
        }
    }
}
