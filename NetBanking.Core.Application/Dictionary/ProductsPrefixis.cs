using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Application.Dictionary
{
    public static class ProductPrefixis
    {

        public static Dictionary<string, string> ProductPrefixDictionary = new Dictionary<string, string>
    {
        { "CreditCards", "001" },
        { "SavingAccount", "002" },
        {"Transactions", "003" }
    };

    }
   
}
