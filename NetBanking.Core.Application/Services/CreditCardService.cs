using AutoMapper;
using NetBanking.Application.Interfaces.Services;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.CreditCard;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Application.Services
{
    public class CreditCardService : GenericService<SaveCreditCardVM, CreditCardVM, CreditCard>, ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IAccountService _accountServices;
        private readonly IMapper _mapper;

        public CreditCardService(ICreditCardRepository creditCardRepository, IMapper mapper, IAccountService accountServices) : base(creditCardRepository, mapper)
        {
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
            _accountServices = accountServices;
        }

        public override async Task<List<CreditCardVM>> GetAllViewModel()
        {
            var list = await _creditCardRepository.GetAllAsync();

            List<CreditCardVM> result = new List<CreditCardVM>();

            foreach (var item in list)
            {

                CreditCardVM vm = new CreditCardVM
                {
                    Id = item.Id,
                    UserNameofOwner = item.UserNameofOwner,
                    Limit = item.Limit,
                    CurrentAmount = item.CurrentAmount,
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
