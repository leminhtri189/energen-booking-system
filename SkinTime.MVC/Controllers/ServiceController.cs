using Microsoft.AspNetCore.Mvc;

namespace SkinTime.MVC.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
