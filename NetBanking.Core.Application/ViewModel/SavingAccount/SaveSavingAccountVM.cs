

using NetBanking.Core.Application.Dtos.User;

namespace NetBanking.Core.Application.ViewModel.SavingAccount
{
    public class SaveSavingAccountVM
    {
        public int Id { get; set; }
        public string? IdentifyingNumber { get; set; }
        public string? UserName { get; set; }
        public string UserNameofOwner { get; set; }
        public bool IsPrincipal { get; set; } = false;
        public decimal Amount { get; set; }
        public List<UserDTO>? users { get; set; }
        
    }
}
