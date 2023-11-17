using Microsoft.AspNetCore.Mvc;
using NetBanking.Application.Interfaces.Services;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.Services;
using NetBanking.Core.Application.ViewModel.Beneficiary;

namespace NetBanking.Controllers
{
    public class BeneficiaryController : Controller
    {
        private readonly IBeneficiaryService _service;
        private readonly IAccountService _accountService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IUserService _userService;

        public BeneficiaryController(IAccountService accountService, ISavingAccountService savingAccountService, IBeneficiaryService service, IUserService userService)
        {

            _accountService = accountService;
            _savingAccountService = savingAccountService;
            _service = service;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllViewModel();
            return View("Beneficiary", list);
        }

        public IActionResult AddBeneficiary()
        {
            var viewModel = new SaveBeneficiaryVM();
            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> AddBeneficiary(SaveBeneficiaryVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var addedBeneficiary = await _service.Add(viewModel);

                    return RedirectToAction("AddBeneficiary");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Hubo un error al agregar el beneficiario: " + ex.Message);
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBeneficiary(int ID)
        {
            await _service.Delete(ID);
            return View("Beneficiary",await _service.GetAllViewModel());
        }

    }
}
