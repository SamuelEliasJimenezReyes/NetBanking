using AutoMapper;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Application.ViewModels.User;
using NetBanking.Core.Domain.Entities;
namespace NetBanking.Core.Application.Services
{
    public class SavingAccountService : GenericService<UpdateSavingAccountVM, SavingAccountVM, SavingAccount>, ISavingAccountService
    {
        private readonly ISavingAccountRepository _repository;
        private readonly IMapper _mapper;

        public SavingAccountService(ISavingAccountRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> AddAmountToSavingAccount(UpdateSavingAccountVM vm)
        {
            var savingAccount = await _repository.GetSavingAccountByOwner(vm.UserNameofOwner);

            if (savingAccount != null)
            {
                
                savingAccount.Amount += vm.AmountToAdd;
                await _repository.UpdateAsync(savingAccount, savingAccount.Id);
                return true;
            }

            return false; 
        }
    }
}
