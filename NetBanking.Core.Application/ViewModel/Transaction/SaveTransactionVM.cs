

using System.ComponentModel.DataAnnotations;

namespace NetBanking.Core.Application.ViewModel.Transaction
{
    public class SaveTransactionVM
    {
        public string OriginAccountNumber { get; set; } = null!;

        [StringLength(9, ErrorMessage = "El número de cuenta debe tener exactamente 9 dígitos.")]
        public string DestinationAccountNumber { get; set; } = null!;

        [Range(1, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 1.")]
        public decimal Amount { get; set; }
        public string? UserNameOfAccountHolder { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string? Description { get; set; } = null!;
        public int? TransactionTypeId { get; set; }
    }
}
