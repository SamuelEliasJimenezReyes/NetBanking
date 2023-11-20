using NetBanking.Core.Application.Dtos.User;
using NetBanking.Core.Application.ViewModel.Transaction;
using System.ComponentModel.DataAnnotations;

namespace NetBanking.Core.Application.ViewModel.CreditCard
{
    public class SaveCreditCardVM
    {
        public int Id { get; set; }
        public string? IdentifyingNumber { get; set; } 

        public string UserNameofOwner { get; set; }
        [Required(ErrorMessage = "Es requerido")]
        public SaveTransactionVM? SaveTransactionVM { get; set; }
        public string? UserName {get; set; } 
        public decimal Limit { get; set; }
        public decimal Debt { get; set; }
        public decimal CurrentAmount { get; set; }

        public List<UserDTO>? users;
    }
}
