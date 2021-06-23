using System;
using System.Collections.Generic;

#nullable disable

namespace SycamoreWebApp.Models
{
    public partial class EmployerDetail
    {
        public int EmployerDetailsId { get; set; }
        public string EmployerName { get; set; }
        public string PostalCode { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
    }
}
