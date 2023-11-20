﻿using NetBanking.Core.Application.Dtos.User;
using NetBanking.Core.Application.ViewModel.Transaction;
using System.ComponentModel.DataAnnotations;

namespace NetBanking.Core.Application.ViewModel.Loan
{
    public class SaveLoanVM
    {
        public int Id {get; set;}
        public string? IdentifyingNumber { get; set; } = null!;
        [Required(ErrorMessage = "El monto debe ser mayor que 1.")]
        public string UserNameofOwner { get; set; } = null!;

        public string? UserName {get; set; } =null!;
        [Range(1, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 1.")]
        public decimal LoanQuantity { get; set; }
        public decimal? PaidQuantity { get; set; }

        public List<UserDTO>? users;
        [Required(ErrorMessage = "Es requerido")]
        public SaveTransactionVM? SaveTransactionVM { get; set; }
        public bool Status { get; set; } = true;
    }
}
