using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace adm_commerce.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]

    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }

}
