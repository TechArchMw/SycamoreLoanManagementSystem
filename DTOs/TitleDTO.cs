using System;

namespace SycamoreWebApp.DTOs
{
    public class TitleDTO
    {
        public string TitleCode { get; set; }
        public string TitleName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
    }
}
