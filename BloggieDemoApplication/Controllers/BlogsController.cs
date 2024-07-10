using Microsoft.AspNetCore.Mvc;

namespace BloggieDemoApplication.Controllers
{
    public class BlogsController : Controller
    {
        [HttpGet]
        public IActionResult Index(string urlHandle)
        {
            return View();
        }
    }
}
