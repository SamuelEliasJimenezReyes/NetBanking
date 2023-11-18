using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.Transaction;

namespace NetBanking.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ISavingAccountService _savingAccountService;
        private readonly ITransactionService _transactionService;
        private readonly ILoanService _loanService;
        private readonly ICreditCardService _creditCardService;

        public PaymentController(ISavingAccountService savingAccountService, ITransactionService transactionService, ILoanService loanService, ICreditCardService creditCardService)
        {
            _savingAccountService = savingAccountService;
            _transactionService = transactionService;
            _loanService = loanService;
            _creditCardService = creditCardService;
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

        public async Task<IActionResult> PaymentBeneficiary()
        {
            ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
            return View(new SaveTransactionVM());
        }

        public async Task<IActionResult> PaymentCreditCard()
        {
            ViewBag.CreditCards = await _creditCardService.GetAllVMbyUserId();
            ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
            return View(new SaveTransactionVM());
        }

        [HttpPost]
        public async Task<IActionResult> PaymentCreditCard(SaveTransactionVM svm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CreditCards = await _creditCardService.GetAllVMbyUserId();
                return View(new SaveTransactionVM());
            }



            return View();
        }

       

        public async Task<IActionResult> PaymentLoan()
        {
            ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
            return View(new SaveTransactionVM());
        }
    }
}
