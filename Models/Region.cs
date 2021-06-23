using System;
using System.Collections.Generic;

#nullable disable

namespace SycamoreWebApp.Models
{
    public partial class Region
    {
        public Region()
        {
            Districts = new HashSet<District>();
        }

        public int RegionId { get; set; }
        public int RegionName { get; set; }
        public int NationalityId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }

        public virtual Nationality Nationality { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
