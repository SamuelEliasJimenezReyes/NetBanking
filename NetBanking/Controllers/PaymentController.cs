﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IBeneficiaryService _beneficiaryService;

        public PaymentController(ISavingAccountService savingAccountService, ITransactionService transactionService, ILoanService loanService, ICreditCardService creditCardService, IBeneficiaryService beneficiaryService)
        {
            _savingAccountService = savingAccountService;
            _transactionService = transactionService;
            _loanService = loanService;
            _creditCardService = creditCardService;
            _beneficiaryService = beneficiaryService;
        }

        #region Express
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
            if (paymentExpress.HasError)
            {
                ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
                return View(paymentExpress);
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

       

        public async Task<IActionResult> MakePaymentExpress(string AccountHolderId, 
            string OriginAccount,
            string DestinationAccount,
            decimal Amount, string Description, 
            int TransactionTypeId)
        {
            var saveTansactionVM = new SaveTransactionVM
            {
                OriginAccountNumber = OriginAccount,
                DestinationAccountNumber = DestinationAccount,
                Amount = Amount,
                Description = Description,
                TransactionTypeId = TransactionTypeId,
                UserNameOfAccountHolder = AccountHolderId
            };

            await _transactionService.ConfirmExpressPayment(saveTansactionVM);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }
        #endregion

        public async Task<IActionResult> PaymentBeneficiary()
        {
            ViewBag.Beneficiary = await _beneficiaryService.GetBeneficiryByUserId();
            return View(new SaveTransactionVM());
        }

        [HttpPost]
        public async Task<IActionResult> PaymentBeneficiary(SaveTransactionVM svm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
                return View(new SaveTransactionVM());
            }

            var paymentExpress = await _transactionService.AddBeneficiaryPayment(svm);
            if (paymentExpress.HasError)
            {
                ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
                return View(paymentExpress);
            }

            return View("ConfirmBeneficiaryExpress", paymentExpress);
        }

        public IActionResult ConfirmBeneficiaryExpress(SaveTransactionVM ConfirmVM)
        {
            if (!ModelState.IsValid)
            {
                return View(ConfirmVM);
            }

            return View(ConfirmVM);
        }

        [HttpPost]
        public async Task<IActionResult> MakeBeneficiaryExpress(SaveTransactionVM ConfirmVM)
        {
            if (!ModelState.IsValid)
            {
                return View("ConfirmPaymentExpress", ConfirmVM);
            }
          
            await _transactionService.ConfirmBeneficiaryPayment(ConfirmVM);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }








        #region CreditCard

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
            ViewBag.Loans = await _loanService.GetAllVMbyUserId();
            return View(new SaveTransactionVM());
        }

        [HttpPost]
        public async Task<IActionResult> PaymentLoan(SaveTransactionVM svm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
                ViewBag.Loans = await _loanService.GetAllVMbyUserId();
                return View(new SaveTransactionVM());
            }

            var paymentLoan = await _transactionService.AddLoanPayment(svm);
            if (paymentLoan.SaveTransactionVM.HasError )
            {
                ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
                ViewBag.Loans = await _loanService.GetAllVMbyUserId();
                return View(paymentLoan.SaveTransactionVM);
            }

            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }

        public async Task<IActionResult> Transaction()
        {
            ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
            return View(new SaveTransactionVM());
        }

        [HttpPost]
        public async Task<IActionResult> Transaction(SaveTransactionVM svm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
                return View(new SaveTransactionVM());
            }

            var transaction = await _transactionService.AddTransactionBetween(svm);
            if (transaction.HasError)
            {
                ViewBag.SavingAccounts = await _savingAccountService.GetAllVMbyUserId();
                return View(svm);
            }

            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }
    }
}
#endregion