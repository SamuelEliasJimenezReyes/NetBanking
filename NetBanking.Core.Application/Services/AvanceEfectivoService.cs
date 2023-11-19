using AutoMapper;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.AvancedeEfectivo;
using NetBanking.Core.Application.ViewModel.SavingAccount;

namespace NetBanking.Core.Application.Services
{
    public class AvanceEfectivoService : IAvancedeEfectivo
    {
        private readonly ISavingAccountService _savingAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly IMapper _mapper;

        public AvanceEfectivoService(ISavingAccountService savingAccountService, ICreditCardService creditCardService, IMapper mapper)
        {
            _savingAccountService = savingAccountService;
            _creditCardService = creditCardService;
            _mapper = mapper;
        }

        public async Task<SaveAvanceDeEfectivo> MakeAvance(SaveAvanceDeEfectivo model)
        {
            var credicard = await _creditCardService.GetByCardNumber(model.CardNumber);
            var savingAccount = await _savingAccountService.GetVmByAccountNumber(model.DestinationAccount);

            if(model.Amount > credicard.CurrentAmount)
            {
                model.HasError = true;
                model.Error = "ESTE MONTO PASA DEL MONTO DISPONIBLE";
                return model;
            }
            decimal cobrar = model.Amount + (model.Amount * 0.0625m);
            credicard.Debt += cobrar;
            credicard.CurrentAmount = credicard.CurrentAmount - cobrar;
            savingAccount.Amount += model.Amount;

            await _creditCardService.Update(credicard, credicard.Id);
            await _savingAccountService.Update(savingAccount, savingAccount.Id);

            return model;

            
        }
    }
}
