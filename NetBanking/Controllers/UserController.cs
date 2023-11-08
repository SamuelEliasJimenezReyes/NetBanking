using Microsoft.AspNetCore.Mvc;

namespace NetBanking.Controllers
{
    public class UserController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
