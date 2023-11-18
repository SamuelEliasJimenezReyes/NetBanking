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

           
        public async Task<SCPaymentExpressVM> AddExpressPayment(SaveTransactionVM svm)
        {
            var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);
            SCPaymentExpressVM cp = new();
            if (destinationAccount != null) 
            {
                if(originAccount.Amount >= svm.Amount)
                {
                    var user = await _userService.GetUserDTOAsync(destinationAccount.UserNameofOwner);
                    SCPaymentExpressVM confirmPayment = new()
                    {
                        SaveTransactionVM = svm,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                };
                    return confirmPayment;
                }
                else
                {
                    svm.HasError = true;
                    svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";
                    cp.SaveTransactionVM = svm;

                    return cp;
                }

            }
            else
            {
                svm.HasError = true;
                svm.ErrorMessage = "Este número de cuenta no Existe";
                cp.SaveTransactionVM = svm;

                return cp;
            }
        }

        public async Task PayToBeneficiaries(SaveTransactionVM svm)
        {
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);

            if (originAccount == null)
            {
                throw new InvalidOperationException("Cuenta de origen no encontrada");
            }

            var totalAmount = svm.Amount * svm;

            if (originAccount.Amount < totalAmount)
            {
                throw new InvalidOperationException("Saldo insuficiente para realizar los pagos");
            }

            foreach (var beneficiaryAccountNumber in svm)
            {
                var beneficiaryAccount = await _savingAccountService.GetByAccountINumber(beneficiaryAccountNumber);

                if (beneficiaryAccount == null)
                {
                    throw new InvalidOperationException($"Cuenta de beneficiario {beneficiaryAccountNumber} no encontrada");
                }

                originAccount.Amount -= svm.Amount;
                beneficiaryAccount.Amount += svm.Amount;

                await _savingAccountService.Update(originAccount, originAccount.Id);
                await _savingAccountService.Update(beneficiaryAccount, beneficiaryAccount.Id);

                var transaction = new SaveTransactionVM
                {
                    Amount = svm.Amount,
                    DestinationAccountNumber = beneficiaryAccountNumber,
                    OriginAccountNumber = svm.OriginAccountNumber,
                    Description = "Pago a beneficiario"
                };

                await Add(transaction);
            }
        }


    }
}
