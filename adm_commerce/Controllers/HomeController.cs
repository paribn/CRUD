using Microsoft.AspNetCore.Mvc;

namespace adm_commerce.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
