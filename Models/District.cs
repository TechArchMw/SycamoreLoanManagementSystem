using System;
using System.Collections.Generic;

#nullable disable

namespace SycamoreWebApp.Models
{
    public partial class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int RegionId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }

        public virtual Region Region { get; set; }
    }
}
