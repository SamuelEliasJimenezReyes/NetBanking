

namespace NetBanking.Core.Application.ViewModel.SavingAccount
{
    public class SaveSavingAccountVM
    {
        public string UserNameofOwner { get; set; } = null!;
        public bool IsPrincipal { get; set; }
        public decimal Amount { get; set; }
    }
}
