using AutoMapper;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Domain.Entities;

namespace NetBanking.Core.Application.Services
{
    public class SavingAccountService : GenericService<SaveSavingAccountVM, SavingAccountVM, SavingAccount>, ISavingAccountService
    {
        private readonly ISavingAccountRepository _repository;
        private readonly IMapper _mapper;

        public SavingAccountService(ISavingAccountRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task AddAmountToPrincipalSavingAccount(SaveSavingAccountVM vm)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAmountToSavingAccount(string userName, decimal amount)
        {
            var savingAccount = await _repository.GetSavingAccountByOwner(userName);

            if (savingAccount != null)
            {
                
                savingAccount.Amount += amount;
                savingAccount.UserNameofOwner = userName;
                await _repository.UpdateAsync(savingAccount, savingAccount.Id);
                return true;
            }

            return false; 
        }

      
    }
}
