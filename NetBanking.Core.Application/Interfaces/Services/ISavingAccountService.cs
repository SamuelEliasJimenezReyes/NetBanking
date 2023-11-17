
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Domain.Entities;

namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface ISavingAccountService : IGenericService<SaveSavingAccountVM, SavingAccountVM, SavingAccount>
    {
        Task AddAmountToPrincipalSavingAccount(SaveSavingAccountVM vm);
        Task<bool> AddAmountToSavingAccount(string userName, decimal amount);
        Task<List<SavingAccountVM>> GetAllVMbyUserId();
    }
}
