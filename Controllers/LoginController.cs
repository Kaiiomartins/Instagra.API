using Microsoft.AspNetCore.Mvc;

namespace Instagram.API.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
