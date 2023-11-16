

using AutoMapper;
using Microsoft.AspNetCore.Http;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.CreditCard;
using NetBanking.Core.Application.ViewModel.Loan;
using NetBanking.Core.Application.ViewModel.Products;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Domain.Common;

namespace NetBanking.Core.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly ISavingAccountRepository _savingAccountRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductServices(ICreditCardRepository creditCardRepository, 
            ISavingAccountRepository savingAccountRepository, 
            ILoanRepository loanRepository, IMapper mapper, 
            IHttpContextAccessor httpContextAccessor)
        {
            _creditCardRepository = creditCardRepository;
            _savingAccountRepository = savingAccountRepository;
            _loanRepository = loanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }
      
        public async Task<ProductVM> GetAllProducts()
        {
            var credictCardList = await _creditCardRepository.GetAllAsync();
            var savingAccountList = await _savingAccountRepository.GetAllAsync();
            var loanList = await _loanRepository.GetAllAsync();

            credictCardList = credictCardList.Where(x => x.UserNameofOwner == userSession.Id).ToList();
            savingAccountList = savingAccountList.Where(x => x.UserNameofOwner == userSession.Id).ToList();
            loanList = loanList.Where(x => x.UserNameofOwner == userSession.Id).ToList();

            var product = new ProductVM
            {
                CreditCards = _mapper.Map<List<CreditCardVM>>(credictCardList),
                SavingAccounts = _mapper.Map<List<SavingAccountVM>>(savingAccountList),
                LoanVMs = _mapper.Map<List<LoanVM>>(loanList)
            };

            return product;
        }

    }
}
