using System;
using System.Collections.Generic;

#nullable disable

namespace SycamoreWebApp.Models
{
    public partial class PaymentPeriod
    {
        public int PaymentPeriodId { get; set; }
        public int Period { get; set; }
        public bool IsCalculateInMonths { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
    }
}
