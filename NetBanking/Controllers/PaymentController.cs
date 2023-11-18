using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.Transaction;

namespace NetBanking.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ISavingAccountService _savingAccountService;
        private readonly ITransactionService _transactionService;

        public PaymentController(ISavingAccountService savingAccountService, ITransactionService transactionService)
        {
            _savingAccountService = savingAccountService;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> PaymentExpress()
        {
            ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
            return View(new SaveTransactionVM());
        }

        [HttpPost]
        public async Task<IActionResult> PaymentExpress(SaveTransactionVM svm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
                return View(new SaveTransactionVM());
            }

            var paymentExpress = await _transactionService.AddExpressPayment(svm);
            if (paymentExpress.SaveTransactionVM.HasError)
            {
                ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
                return View(paymentExpress.SaveTransactionVM);
            }

            return View("ConfirmPaymentExpress", paymentExpress);
        }

        public IActionResult ConfirmPaymentExpress(SCPaymentExpressVM ConfirmVM)
        {
            if(!ModelState.IsValid)
            {
                return View(new SCPaymentExpressVM());
            }

            return View(ConfirmVM);
        }

        [HttpPost]
        public async Task<IActionResult> MakePaymentExpress(SCPaymentExpressVM ConfirmVM)
        {
            if (!ModelState.IsValid)
            {
                return View("ConfirmPaymentExpress",new SCPaymentExpressVM());
            }
            var confirm = new SaveTransactionVM
            {
                Amount = ConfirmVM.SaveTransactionVM.Amount,
                DestinationAccountNumber = ConfirmVM.SaveTransactionVM.DestinationAccountNumber,
                OriginAccountNumber = ConfirmVM.SaveTransactionVM.OriginAccountNumber
            };

            await _transactionService.ConfirmExpressPayment(confirm);
            return RedirectToRoute(new {controller = "Client", action = "Index"});
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
