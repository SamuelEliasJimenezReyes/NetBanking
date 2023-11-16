
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.SavingAccount;

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


        [HttpPost]
        public async Task<IActionResult> CreateSavingAccounts(SaveSavingAccountVM svm)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            
            await _savingAccountService.Add(svm);

           
            return View();
        }


    }
}
