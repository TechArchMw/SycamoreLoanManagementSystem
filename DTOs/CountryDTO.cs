using System;

namespace SycamoreWebApp.DTOs
{
    public class CountryDTO
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string NumericCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
