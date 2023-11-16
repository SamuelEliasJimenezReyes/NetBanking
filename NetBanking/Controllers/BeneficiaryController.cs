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

        public BeneficiaryController(IBeneficiaryService service, IAccountService accountService)
        {
            _service = service;
            _accountService = accountService;
        }

        public IActionResult AddBeneficiary()
        {
            var viewModel = new SaveBeneficiaryVM();
            return View(viewModel);

        }

    //    [HttpPost]
    //    public async Task<IActionResult> AddBeneficiary(SaveBeneficiaryVM viewModel)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            // Verifica si el número de cuenta existe
    //            //var accountExists = await _accountService.CheckAccountExists(viewModel.IdentifyingNumberofProduct);

    //        //    if (accountExists)
    //        //    {
    //        //                           var addedSuccessfully = await _service.AddBeneficiary(viewModel.IdentifyingNumberofProduct);

    //        //        if (addedSuccessfully)
    //        //        {
    //        //            return RedirectToAction("Index");
    //        //        }
    //        //        else
    //        //        {
    //        //            ModelState.AddModelError(string.Empty, "No se pudo agregar el beneficiario");
    //        //        }
    //        //    }
    //        //    else
    //        //    {
    //        //        ModelState.AddModelError(nameof(viewModel.IdentifyingNumberofProduct), "El número de cuenta no existe");
    //        //    }
    //        //}

    //        return View(viewModel);
    //    }

    }
}
