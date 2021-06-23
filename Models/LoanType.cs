using System;
using System.Collections.Generic;

#nullable disable

namespace SycamoreWebApp.Models
{
    public partial class LoanType
    {
        public int LoanTypeId { get; set; }
        public string LoanDescription { get; set; }
        public decimal? ProcessingFee { get; set; }
        public decimal? Interest { get; set; }
        public decimal? AdminFee { get; set; }
        public decimal? Eir { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
    }
}
