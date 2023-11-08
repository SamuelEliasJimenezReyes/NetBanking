
using Microsoft.AspNetCore.Mvc;

namespace NetBanking.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
