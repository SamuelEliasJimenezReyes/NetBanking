using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.Transaction;
using NetBanking.Core.Application.ViewModels.User;

namespace NetBanking.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ISavingAccountService _savingAccountService;

        public PaymentController(ISavingAccountService savingAccountService)
        {
            _savingAccountService = savingAccountService;
        }

        public async Task<IActionResult> PaymentExpress()
        {
            ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
            return View(new SaveTransactionVM());
        }

        [HttpPost]
        public IActionResult PaymentExpress(SaveTransactionVM svm)
        {
            return View();
        }

        public async Task<IActionResult> PaymentBeneficiary()
        {
            ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
            return View(new SaveTransactionVM());
        }

        public async Task<IActionResult> PaymentCreditCard()
        {
            ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
            return View(new SaveTransactionVM());
        }

        public async Task<IActionResult> PaymentLoan()
        {
            ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
            return View(new SaveTransactionVM());
        }
    }
}
