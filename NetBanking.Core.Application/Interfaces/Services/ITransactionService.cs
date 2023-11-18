using NetBanking.Core.Application.ViewModel.Transaction;

namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface ITransactionService : IGenericService<SaveTransactionVM, TransactionVM, TransactionVM>
    {
        Task<SCPaymentExpressVM> AddExpressPayment(SaveTransactionVM svm);

        Task AddCreditCardPayment(SaveTransactionVM svm);
    }
}
