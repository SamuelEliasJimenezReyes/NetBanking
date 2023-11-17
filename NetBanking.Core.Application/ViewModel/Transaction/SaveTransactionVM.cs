

namespace NetBanking.Core.Application.ViewModel.Transaction
{
    public class SaveTransactionVM
    {
        public string OriginAccountNumber { get; set; } = null!;
        public string DestinationAccountNumber { get; set; } = null!;
        public decimal Amount { get; set; }
        public string? UserNameOfAccountHolder { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string? Description { get; set; } = null!;
        public int? TransactionTypeId { get; set; }
    }
}
