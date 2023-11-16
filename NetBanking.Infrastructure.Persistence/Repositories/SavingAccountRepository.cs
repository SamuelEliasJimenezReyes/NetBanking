using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Domain.Entities;
using NetBanking.Infrastructure.Persistence.Context;


namespace NetBanking.Infrastructure.Persistence.Repositories
{
    public class SavingAccountRepository : GenericRepository<SavingAccount>, ISavingAccountRepository
    {
        public SavingAccountRepository(NetBankingContext dbContext) : base(dbContext)
        {
        }

        public override async Task<SavingAccount> AddAsync(SavingAccount entity)
        {
            var list = await GetAllAsync();

            entity.IdentifyingNumber = ProductNumberGenerator.AlgorithmForProductNumber<SavingAccount>("002", list);

            return await base.AddAsync(entity);
        }


    }
}
