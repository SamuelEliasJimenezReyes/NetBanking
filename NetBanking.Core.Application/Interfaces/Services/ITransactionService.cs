﻿using NetBanking.Core.Application.ViewModel.Loan;
using NetBanking.Core.Application.ViewModel.Transaction;

namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface ITransactionService : IGenericService<SaveTransactionVM, TransactionVM, TransactionVM>
    {
        Task<SCPaymentExpressVM> AddExpressPayment(SaveTransactionVM svm);
        Task ConfirmExpressPayment(SaveTransactionVM svm);
        Task<SaveLoanVM> AddLoanPayment(SaveTransactionVM svm);
        Task<SaveTransactionVM> AddTransactionBetween(SaveTransactionVM svm);
        
    }
}
