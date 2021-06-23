using System;
using System.Collections.Generic;

#nullable disable

namespace SycamoreWebApp.Models
{
    public partial class Loan
    {
        public long LoanId { get; set; }
        public int? LoanTypeId { get; set; }
        public string PurposeOfLoan { get; set; }
        public decimal? Amount { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? NetSalaryAsPerCurrentPaySlip { get; set; }
        public decimal? TotalLoanAmount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
        public int? CustomerId { get; set; }
        public int? LoanStatusId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
