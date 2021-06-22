using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SycamoreWebApp.DTOs
{
    public class LoanDTO
    {
        public long LoanId { get; set; }
        public int? LoanTypeId { get; set; }
        public string PurposeOfLoan { get; set; }
        public string Period { get; set; }
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
        public string Gender { get; set; }
        public string Title { get; set; }
    }
}
