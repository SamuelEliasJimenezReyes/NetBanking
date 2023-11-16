
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Domain.Entities;
using NetBanking.Infrastructure.Persistence.Context;


namespace NetBanking.Infrastructure.Persistence.Repositories
{
    public class BeneficiaryRepository : GenericRepository<Beneficiary>, IBeneficiaryRepository
    {
        public BeneficiaryRepository(NetBankingContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddBeneficiary(string identifyingNumber)
        {
            return true;
        }
    }
}
