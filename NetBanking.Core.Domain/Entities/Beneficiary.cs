using NetBanking.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Domain.Entities
{
    public class Beneficiary : AuditableEntity
    {
        public string IdentifyingNumberofProduct { get; set; } = null!;
        public string BeneficiaryUserName { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
