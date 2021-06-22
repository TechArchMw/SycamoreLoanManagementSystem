using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SycamoreWebApp.DTOs
{
    public class LoanRepaymentScheduleDTO
    {
        public int Period { get; set; }
        public decimal Installment { get; set; }
        public decimal CapitalRepayment { get; set; }
        public decimal CapitalBalance { get; set; }
        public decimal InterestPayment { get; set; }
        public decimal InsurancePayment { get; set; }
        public decimal AdminFee { get; set; }
        public decimal CRIIA { get; set; }


    }
}
 