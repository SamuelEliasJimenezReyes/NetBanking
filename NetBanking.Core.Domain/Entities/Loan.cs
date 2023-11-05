﻿using NetBanking.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Domain.Entities
{
    public class Loan : Product
    {
        public decimal LoanQuantity { get; set; }
        public decimal PaidQuantity { get; set; }

    }
}
