﻿using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.ViewModels.User;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.NetBanking.Middlewares;
using NetBanking.Core.Application.Dtos.User;
using NetBanking.Core.Application.ViewModel.User;

namespace WebApp.NetBanking.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }

        
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            var origin = Request.Headers["origin"];

            RegisterResponse response = new();

            if (ModelState.IsValid)
            {
                response = await _userService.RegisterAsync(vm, origin);
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(vm);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult ResetPassword(string token, string email)
        {
            return View(new ResetPasswordViewModel { Token = token, Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (vm.Email == null && vm.Password == null && vm.ConfirmPassword == null)
            {
                return View(vm);
            }
            
            ResetPasswordResponse response = await _userService.ResetPasswordAsync(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }

        public async Task UpdateAdmin(UserDTO dtO)
        {
           await _userService.UpdateUserByEmail(dtO);
        }

        public async Task UpdateClient(EditUserViewModel vm)
        {
            SaveUserViewModel value = new();
            var sum = value.InitialAmount + vm.InitialAmount;
            
            value.Cedula= vm.Cedula;
            value.Phone= vm.Phone;
            value.Email= vm.Email;
            value.InitialAmount= sum;
            value.FirstName = value.FirstName;
            value.LastName= value.LastName;

            await _userService.UpdateUserByUserName(vm);

        }
    }
}

