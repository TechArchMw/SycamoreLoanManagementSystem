using System;
using System.Collections.Generic;

#nullable disable

namespace SycamoreWebApp.Models
{
    public partial class Nationality
    {
        public Nationality()
        {
            Regions = new HashSet<Region>();
        }

        public int NationalityId { get; set; }
        public string NationalityName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
