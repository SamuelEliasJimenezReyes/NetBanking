using NetBanking.Core.Application.ViewModel.Transaction;

namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface ITransactionService : IGenericService<SaveTransactionVM, TransactionVM, TransactionVM>
    {
        Task<SaveTransactionVM> AddExpressPayment(SaveTransactionVM svm);
        Task ConfirmExpressPayment(SaveTransactionVM svm);
        Task ConfirmBeneficiaryPayment(SaveTransactionVM svm);
        Task<SaveTransactionVM> AddBeneficiaryPayment(SaveTransactionVM svm);
    }
}
