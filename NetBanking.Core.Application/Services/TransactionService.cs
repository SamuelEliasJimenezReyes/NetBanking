using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.Loan;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Application.ViewModel.Transaction;
using NetBanking.Core.Domain.Entities;
using TransactionType = NetBanking.Core.Application.Enums.TransactionType;

namespace NetBanking.Core.Application.Services
{

    public class TransactionService : GenericService<SaveTransactionVM, TransactionVM, Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IUserService _userService;
        private readonly ILoanService _loanService;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, ISavingAccountService savingAccountService, IUserService userService, ILoanService loanService) : base(transactionRepository, mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _savingAccountService = savingAccountService;
            _userService = userService;
            _loanService = loanService;
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
                if (originAccount.Amount >= svm.Amount)
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


        public async Task<SaveLoanVM> AddLoanPayment(SaveTransactionVM svm)
        {
            var destinationAccount = await _loanService.GetByLoanINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);
            SaveLoanVM loan = new();

            if(originAccount.Amount >= svm.Amount)
            {
                var pay = destinationAccount.PaidQuantity + svm.Amount;

                if(pay != destinationAccount.LoanQuantity)
                {
                    var amount = 0m; 
                    if (pay < destinationAccount.LoanQuantity)
                    {
                        pay = destinationAccount.PaidQuantity += svm.Amount;
                        originAccount.Amount -= svm.Amount;
                    }
                    else
                    {                  
                        amount = pay - destinationAccount.LoanQuantity;
                        destinationAccount.PaidQuantity += svm.Amount - amount;
                        originAccount.Amount -= svm.Amount - amount;
                    }


                    var accountOrigin = new SaveSavingAccountVM()
                    {
                        IdentifyingNumber = originAccount.IdentifyingNumber,
                        Amount = originAccount.Amount,
                        IsPrincipal = originAccount.IsPrincipal,
                        UserNameofOwner = originAccount.UserNameofOwner,    
                        Id  = originAccount.Id
                    };
                    await _savingAccountService.Update(accountOrigin, accountOrigin.Id);


                    var destinyAccount = new SaveLoanVM()
                    {
                        IdentifyingNumber = destinationAccount.IdentifyingNumber,
                        UserNameofOwner = destinationAccount.UserNameofOwner,
                        LoanQuantity = destinationAccount.LoanQuantity,
                        PaidQuantity = destinationAccount.PaidQuantity,
                        Id = destinationAccount.Id
                        
                    };                        
                    await _loanService.Update(destinyAccount, destinyAccount.Id);

                    var transaction = new SaveTransactionVM
                    {
                        Amount = svm.Amount,
                        DestinationAccountNumber = svm.DestinationAccountNumber,
                        OriginAccountNumber = svm.OriginAccountNumber,
                        Description = svm.Description,
                        TransactionTypeId = (int)TransactionType.PagoExpreso
                    };

                    SaveLoanVM saveLoanVM = new()
                    {
                        PaidQuantity = pay,
                        LoanQuantity = amount
                    };

                    await Add(transaction);
                    return saveLoanVM;
                }
                else
                {
                    svm.HasError = true;
                    svm.ErrorMessage = "El prestamo ya ha sido saldado";
                    loan.SaveTransactionVM = svm;
                    return loan;
                }
                
              
            }
            else
            {
                svm.HasError = true;
                svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";
                loan.SaveTransactionVM = svm;
                return loan;
            }
           
        }

        public async Task PayToBeneficiaries(SaveTransactionVM svm)
        {
            /*var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);

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
                };*/

            //await Add(transaction);

        }






        public async Task<SaveTransactionVM> AddTransactionBetween(SaveTransactionVM svm)
        {
            var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);

            if(originAccount.Id != destinationAccount.Id)
            {
                if (originAccount.Amount >= svm.Amount)
                {

                    destinationAccount.Amount += svm.Amount;

                    var destinyAccount = new SaveSavingAccountVM()
                    {
                        IdentifyingNumber = destinationAccount.IdentifyingNumber,
                        Amount = destinationAccount.Amount,
                        IsPrincipal = destinationAccount.IsPrincipal,
                        UserNameofOwner = destinationAccount.UserNameofOwner,
                        Id = destinationAccount.Id
                    };
                    await _savingAccountService.Update(destinyAccount, destinyAccount.Id);

                    originAccount.Amount -= svm.Amount;


                    var accountOrigin = new SaveSavingAccountVM()
                    {
                        IdentifyingNumber = originAccount.IdentifyingNumber,
                        Amount = originAccount.Amount,
                        IsPrincipal = originAccount.IsPrincipal,
                        UserNameofOwner = originAccount.UserNameofOwner,
                        Id = originAccount.Id
                    };
                    await _savingAccountService.Update(accountOrigin, accountOrigin.Id);

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
                svm.ErrorMessage = "No puede realizar una transferencia a la misma cuenta";

                return svm;
            }
           
        }
    }
}
