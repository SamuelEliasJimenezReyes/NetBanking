

using NetBanking.Core.Domain.Common;
using System.Security.Principal;

namespace NetBanking.Core.Domain.Entities
{
    public class Transaction : AuditableEntity
    {
        public string OriginAccountNumber { get; set; } = null!;
        public string DestinationAccountNumber { get; set; } = null!;
        public decimal Amount { get; set; } 
        public string UserNameOfAccountHolder { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; } = null!;
    }
}
