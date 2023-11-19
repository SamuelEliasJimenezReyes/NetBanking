using AutoMapper;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Application.ViewModel.Transaction;
using NetBanking.Core.Domain.Entities;
using System.Windows.Markup;

namespace NetBanking.Core.Application.Services
{

    public class TransactionService : GenericService<SaveTransactionVM, TransactionVM, Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IUserService _userService;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, ISavingAccountService savingAccountService, IUserService userService) : base(transactionRepository, mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _savingAccountService = savingAccountService;
            _userService = userService;
        }

        public async Task ConfirmExpressPayment(SaveTransactionVM svm)
        {
            var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);

            destinationAccount.Amount += svm.Amount;
            var destinyAccount = _mapper.Map<SaveSavingAccountVM>(destinationAccount);
            await _savingAccountService.Update(destinyAccount, destinyAccount.Id);

            originAccount.Amount -= svm.Amount;
            var accountOrigin = _mapper.Map<SaveSavingAccountVM>(originAccount);
            await _savingAccountService.Update(accountOrigin, accountOrigin.Id);

            var transaction = new SaveTransactionVM
            {
                Amount = svm.Amount,
                DestinationAccountNumber = svm.DestinationAccountNumber,
                OriginAccountNumber = svm.OriginAccountNumber,
                Description = svm.Description,
            };

            await Add(transaction);
        }


        public async Task<SaveTransactionVM> AddExpressPayment(SaveTransactionVM svm)
        {
            var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);
            if (destinationAccount != null)
            {
                if (originAccount.Amount >= svm.Amount)
                {
                    var user = await _userService.GetUserDTOAsync(destinationAccount.UserNameofOwner);
                    svm.FirstName = user.FirstName;
                    svm.LastName = user.LastName;
                    return svm;
                }
                else
                {
                    svm.HasError = true;
                    svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";

                    return svm;
                }

            }
            else
            {
                svm.HasError = true;
                svm.ErrorMessage = "Este número de cuenta no Existe";

                return svm;
            }
        }

        public async Task<SaveTransactionVM> AddBeneficiaryPayment(SaveTransactionVM svm)
        {
            var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);
            if (destinationAccount != null)
            {
                if (originAccount.Amount >= svm.Amount)
                {
                    var user = await _userService.GetUserDTOAsync(destinationAccount.UserNameofOwner);
                    svm.FirstName = user.FirstName;
                    svm.LastName = user.LastName;
                    return svm;
                }
                else
                {
                    svm.HasError = true;
                    svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";

                    return svm;
                }

            }
            else
            {
                svm.HasError = true;
                svm.ErrorMessage = "Este número de cuenta no Existe";

                return svm;
            }
        }

        public async  Task ConfirmBeneficiaryPayment(SaveTransactionVM svm)
        {
            var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);

            destinationAccount.Amount += svm.Amount;
            var destinyAccount = _mapper.Map<SaveSavingAccountVM>(destinationAccount);
            await _savingAccountService.Update(destinyAccount, destinyAccount.Id);

            originAccount.Amount -= svm.Amount;
            var accountOrigin = _mapper.Map<SaveSavingAccountVM>(originAccount);
            await _savingAccountService.Update(accountOrigin, accountOrigin.Id);

            var transaction = new SaveTransactionVM
            {
                Amount = svm.Amount,
                DestinationAccountNumber = svm.DestinationAccountNumber,
                OriginAccountNumber = svm.OriginAccountNumber,
                Description = svm.Description,
            };

            await Add(transaction);
        }
    }


}
