using AutoMapper;
using NetBanking.Application.Interfaces.Services;
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
        private readonly IAccountService _accountServices;

        public SavingAccountService(ISavingAccountRepository repository, IMapper mapper, IAccountService accountServices) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _accountServices = accountServices;
        }

        public override async Task<List<SavingAccountVM>> GetAllViewModel()
        {
            var list = await _repository.GetAllAsync();

            List<SavingAccountVM> result = new List<SavingAccountVM>();

            foreach (var item in list)
            {

                SavingAccountVM vm = new SavingAccountVM
                {
                    Id = item.Id,
                    UserNameofOwner = item.UserNameofOwner,
                    Amount = item.Amount,
                    IsPrincipal = item.IsPrincipal,
                    IdentifyingNumber = item.IdentifyingNumber,
                };

                var a = await _accountServices.GetUserById(item.UserNameofOwner);

                vm.UserName = a.UserName;
                result.Add(vm);

            }

            return result;
        }
    }
}
