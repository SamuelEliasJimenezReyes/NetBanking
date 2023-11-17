

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
        private readonly ICreditCardService _creditCardService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly ILoanService _loanService;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductServices(ICreditCardService creditCardService, 
            ISavingAccountService savingAccountService, 
            ILoanService loanService, IMapper mapper, 
            IHttpContextAccessor httpContextAccessor)
        {
            _creditCardService = creditCardService;
            _savingAccountService = savingAccountService;
            _loanService = loanService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }
      
        public async Task<ProductVM> GetAllProducts()
        {
            var credictCardList = await _creditCardService.GetAllVMbyUserId();
            var savingAccountList = await _savingAccountService.GetAllVMbyUserId();
            var loanList = await _loanService.GetAllVMbyUserId();

            
            var product = new ProductVM
            {
                CreditCards = credictCardList,
                SavingAccounts = savingAccountList,
                LoanVMs = loanList
            };

            return product;
        }

    }
}
