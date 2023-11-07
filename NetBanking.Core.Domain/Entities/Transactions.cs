﻿using NetBanking.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Domain.Entities
{
    public class Transactions : AuditableEntity
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
