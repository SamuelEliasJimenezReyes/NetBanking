
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.CreditCard;
using NetBanking.Core.Application.ViewModel.Loan;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Domain.Entities;

namespace NetBanking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly ILoanService _loanService;
        

        public AdminController(IUserService userService, ISavingAccountService savingAccountService, ICreditCardService creditCardService, ILoanService loanService)
        {
            _userService = userService;
            _savingAccountService = savingAccountService;
            _creditCardService = creditCardService;
            _loanService = loanService;

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

        public async Task<IActionResult> UpdateFilter(string newFilter)
        {
            string filter;
            if (newFilter == "Admin")
            {
                filter = "Client";
            }
            else
            {
                filter = "Admin";
            }
            ViewBag.Filter = filter;
            var list = await _userService.GetAllUsers();
            return View("Index",list);
        }


        public async Task<IActionResult> SavingAccounts()
        {
            var list = await _savingAccountService.GetAllViewModel();
            await _userService.GetAllUsers();
            return View(list);
        }

        public async Task<IActionResult> CreditCards()
        {
            var list = await  _creditCardService.GetAllViewModel() ;
            return View(list);
        }

        public async Task<IActionResult> Loans()
        {
            var list = await _loanService.GetAllViewModel();
            return View(list);

        }
        public async Task<IActionResult> CreateSavingAccounts()
        {
            SaveSavingAccountVM svm = new SaveSavingAccountVM();

            svm.users = await _userService.GetAllUsers();
         
            
            return View(svm);
        }

        public async Task<IActionResult> CreateCreditCards()
        {
            SaveCreditCardVM svm = new SaveCreditCardVM();

            svm.users = await _userService.GetAllUsers();


            return View(svm);
        }

        public async Task<IActionResult> CreateLoans()
        {
            SaveLoanVM svm = new SaveLoanVM();

            svm.users = await _userService.GetAllUsers();

            return View(svm);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSavingAccounts(SaveSavingAccountVM svm)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            
            await _savingAccountService.Add(svm);
            var list = await _savingAccountService.GetAllViewModel();

            return View("SavingAccounts", list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCreditCards(SaveCreditCardVM svm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            await _creditCardService.Add(svm);
            var list = await _savingAccountService.GetAllViewModel();

            return View("SavingAccounts", list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoans(SaveLoanVM svm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            await _loanService.Add(svm);
            var list = await _savingAccountService.GetAllViewModel();

            return View("SavingAccounts", list);
        }



        public async Task<IActionResult> DeleteSavingAccounts(int id)
        {
            await _savingAccountService.Delete(id);
            var list = await _savingAccountService.GetAllViewModel();
            return View("SavingAccounts", list);

        }


    }
}
