using Microsoft.AspNetCore.Mvc;

namespace OmniMarket.Admin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
