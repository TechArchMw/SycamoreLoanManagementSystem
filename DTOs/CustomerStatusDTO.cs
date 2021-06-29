using System;

namespace SycamoreWebApp.DTOs
{
    public class CustomerStatusDTO
    {
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
