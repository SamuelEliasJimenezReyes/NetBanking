using NetBanking.Core.Application.Dtos.User;


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
    }
}
