using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
