
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.Interfaces.Services;

namespace NetBanking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _userService.GetAllUsers();
            return View(list);
        }

        public IActionResult DashBoard()
        {
            return View();
        }

        public async Task<IActionResult> ChangeUserStatus(string userName)
        {
            await _userService.ChangeUserStatus(userName);
            var list = await _userService.GetAllUsers();
            return View("Index",list);
        }

        public async Task<IActionResult> UpdateFilter(bool newFilter)
        {
            bool filter;
            if (newFilter == true)
            {
                filter = true;
            }
            else
            {
                filter = false;
            }
            ViewBag.Filter = filter;
            var list = await _userService.GetAllUsers();
            return View("Index",list);
        }

    }
}
