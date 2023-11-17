using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Application.ViewModel.Beneficiary
{
    public class SaveBeneficiaryVM
    {
        [Required(ErrorMessage = "El número de cuenta es requerido")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El número de cuenta debe tener 9 dígitos")]
        public string IdentifyingNumberofProduct { get; set; }
    }
}
