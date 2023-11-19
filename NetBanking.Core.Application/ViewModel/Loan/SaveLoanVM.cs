using NetBanking.Core.Application.Dtos.User;
using NetBanking.Core.Application.ViewModel.Transaction;

namespace NetBanking.Core.Application.ViewModel.Loan
{
    public class SaveLoanVM
    {
        public int Id {get; set;}
        public string? IdentifyingNumber { get; set; } = null!;
        public string UserNameofOwner { get; set; } = null!;

        public string? UserName {get; set; } = null!;
        public decimal LoanQuantity { get; set; }
        public decimal? PaidQuantity { get; set; }

        public List<UserDTO>? users;
        public SaveTransactionVM? SaveTransactionVM { get; set; }
        public int Status { get; set; }
    }
}
