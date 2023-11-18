using AutoMapper;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.Transaction;
using NetBanking.Core.Domain.Entities;

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

        //public async Task<SCPaymentExpressVM> AddExpressPayment(SaveTransactionVM svm)
        //{
        //    var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.DestinationAccountNumber);
        //    var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);
        //    SCPaymentExpressVM cp = new();
        //    if (destinationAccount != null)
        //    {
        //        if (originAccount.Amount >= svm.Amount)
        //        {
        //            var user = await _userService.GetUserDTOAsync(destinationAccount.UserNameofOwner);
        //            SCPaymentExpressVM confirmPayment = new()
        //            {
        //                SaveTransactionVM = svm,
        //                FirstName = user.FirstName,
        //                LastName = user.LastName,
        //            };
        //            return confirmPayment;
        //        }
        //        else
        //        {
        //            svm.HasError = true;
        //            svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";
        //            cp.SaveTransactionVM = svm;

        //            return cp;
        //        }

        //    }
        //    else
        //    {
        //        svm.HasError = true;
        //        svm.ErrorMessage = "Este número de cuenta no Existe";
        //        cp.SaveTransactionVM = svm;

        //        return cp;
        //    }
        //}

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
    }
}
