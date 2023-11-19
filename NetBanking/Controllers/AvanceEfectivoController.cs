﻿using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.AvancedeEfectivo;
using NetBanking.Core.Application.Helpers;

namespace NetBanking.Controllers
{
    public class AvanceEfectivoController : Controller
    {
        private readonly IAvancedeEfectivo _avancedeEfectivo;
        private readonly ISavingAccountService _savingAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? user;

        public AvanceEfectivoController(IAvancedeEfectivo avancedeEfectivo, ISavingAccountService savingAccountService, ICreditCardService creditCardService, IHttpContextAccessor httpContextAccessor)
        {
            _avancedeEfectivo = avancedeEfectivo;
            _savingAccountService = savingAccountService;
            _creditCardService = creditCardService;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task< IActionResult> Index()
        {
            ViewBag.CreditCard = await _creditCardService.GetAllVMbyUserId();
            ViewBag.Account = await _savingAccountService.GetAllVMbyUserId();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SaveAvanceDeEfectivo model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var avance = await _avancedeEfectivo.MakeAvance(model);
            if (avance.HasError)
            {
                return View(model);
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}