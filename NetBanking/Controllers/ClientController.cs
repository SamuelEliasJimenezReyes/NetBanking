
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.Interfaces.Services;

namespace NetBanking.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ClientController : Controller
    {
        private readonly IUserService _userService;

        public ClientController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _userService.GetAllUsers();
            return View(list);
        }


    }
}
