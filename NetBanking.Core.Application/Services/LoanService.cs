using AutoMapper;
using NetBanking.Application.Interfaces.Services;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.CreditCard;
using NetBanking.Core.Application.ViewModel.Loan;
using NetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Application.Services
{
    public class LoanService : GenericService<SaveLoanVM, LoanVM, Loan>, ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountServices;

        public LoanService(ILoanRepository loanRepository, IMapper mapper, IAccountService accountServices) : base(loanRepository, mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _accountServices = accountServices;
        }

        public override async Task<List<LoanVM>> GetAllViewModel()
        {
            var list = await _loanRepository.GetAllAsync();

            List<LoanVM> result = new List<LoanVM>();

            foreach (var item in list)
            {

                LoanVM vm = new LoanVM
                {
                    Id = item.Id,
                    UserNameofOwner = item.UserNameofOwner,
                    LoanQuantity = item.LoanQuantity,
                    PaidQuantity = item.PaidQuantity,
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
