using AutoMapper;
using Microsoft.Win32.SafeHandles;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Application.ViewModel.Transaction;
using NetBanking.Core.Domain.Entities;
using System.Data;
using System.Windows.Markup;

namespace NetBanking.Core.Application.Services
{

    public class TransactionService : GenericService<SaveTransactionVM, TransactionVM, Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IUserService _userService;
        private readonly ICreditCardRepository _creditCardRepository;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, 
            ISavingAccountService savingAccountService, IUserService userService, ICreditCardRepository creditCardRepository) : base(transactionRepository, mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _savingAccountService = savingAccountService;
            _userService = userService;
            _creditCardRepository = creditCardRepository;
        }

        public async Task ConfirmExpressPayment(SCPaymentExpressVM svm)
        {
            var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.SaveTransactionVM.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.SaveTransactionVM.OriginAccountNumber);

            destinationAccount.Amount += svm.SaveTransactionVM.Amount;
            var destinyAccount = _mapper.Map<SaveSavingAccountVM>(destinationAccount);
            await _savingAccountService.Update(destinyAccount, destinyAccount.Id);

            originAccount.Amount -= svm.SaveTransactionVM.Amount;
            var accountOrigin = _mapper.Map<SaveSavingAccountVM>(originAccount);
            await _savingAccountService.Update(accountOrigin, accountOrigin.Id);

            var transaction = new SaveTransactionVM
            {
                Amount = svm.SaveTransactionVM.Amount,
                DestinationAccountNumber = svm.SaveTransactionVM.DestinationAccountNumber,
                OriginAccountNumber = svm.SaveTransactionVM.OriginAccountNumber,
                Description = svm.SaveTransactionVM?.Description,
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

        public async Task AddCreditCardPayment(SaveTransactionVM svm)
        {

            var destination = await _creditCardRepository.GetByNumber(svm.DestinationAccountNumber);

            var origin = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);

            decimal creditdeuda = destination.Limit - destination.CurrentAmount;

            
            if(svm.Amount > origin.Amount)
            {
                svm.HasError = true;
                svm.ErrorMessage = "No tienes dinero suficiente para ese pago";
            }

            if (svm.Amount > creditdeuda)
            {
                destination.CurrentAmount = destination.Limit;

                decimal excedente = svm.Amount - creditdeuda;
                origin.Amount = excedente;

                
            
            }



           


           

        }

      
    }
}
