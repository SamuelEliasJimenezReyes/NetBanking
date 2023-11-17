using AutoMapper;
using Microsoft.AspNetCore.Http;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.Beneficiary;
using NetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Application.Services
{
    public class BeneficiaryService : GenericService<SaveBeneficiaryVM, BeneficiaryVM, Beneficiary>, IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly  ISavingAccountService _savingAccountService;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IMapper mapper, ISavingAccountService savingAccountService, IHttpContextAccessor httpContextAccessor) : base(beneficiaryRepository, mapper)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _mapper = mapper;
            _savingAccountService = savingAccountService;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<bool> AddBeneficiary(string identifyingNumber)
        { 
            var addedSuccessfully = await _beneficiaryRepository.AddBeneficiary(identifyingNumber);
            return addedSuccessfully;
        }

        public override async Task<SaveBeneficiaryVM> Add(SaveBeneficiaryVM vm)
        {
            var savingAccounts = await _savingAccountService.GetAllViewModel();

            savingAccounts.Where(x =>x.IdentifyingNumber == vm.IdentifyingNumberofProduct);
            return base.Add(vm);
        }
    }
}
