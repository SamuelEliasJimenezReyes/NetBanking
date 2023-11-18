using NetBanking.Core.Application.Dtos.User;

namespace NetBanking.Core.Application.ViewModel.CreditCard
{
    public class SaveCreditCardVM
    {
        public int Id { get; set; }
        public string? IdentifyingNumber { get; set; } 
        public string UserNameofOwner { get; set; } 

        public string? UserName {get; set; } 
        public decimal Limit { get; set; }
        public decimal Debt { get; set; }
        public decimal CurrentAmount { get; set; }

        public List<UserDTO>? users;
    }
}
