using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.AvancedeEfectivo;

namespace NetBanking.Controllers
{
    public class AvanceEfectivoController : Controller
    {
        private readonly IAvancedeEfectivo _avancedeEfectivo;

        public AvanceEfectivoController(IAvancedeEfectivo avancedeEfectivo)
        {
            _avancedeEfectivo = avancedeEfectivo;
        }

        public async Task< IActionResult> Index()
        {
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
            if (model.HasError)
            {
                return View(model);
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
            return View();
        }
    }
}
