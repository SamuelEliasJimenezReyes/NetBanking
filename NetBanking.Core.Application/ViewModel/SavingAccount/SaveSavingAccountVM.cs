

using NetBanking.Core.Application.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace NetBanking.Core.Application.ViewModel.SavingAccount
{
    public class SaveSavingAccountVM
    {
        public int Id { get; set; }
        public string? IdentifyingNumber { get; set; }
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Debe Seleccionar un Usuario")]
        public string UserNameofOwner { get; set; }
        public bool IsPrincipal { get; set; } = false;

        [Required(ErrorMessage = "Debe Introducir un Monto")]
        public decimal Amount { get; set; }
        public List<UserDTO>? users { get; set; }
        
    }
}
