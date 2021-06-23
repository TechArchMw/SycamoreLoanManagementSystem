using System;
using System.Collections.Generic;

#nullable disable

namespace SycamoreWebApp.Models
{
    public partial class NextOfKin
    {
        public int NextOfKinId { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string PhysicalLocation { get; set; }
        public int RelationshipId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }

        public virtual Relationship Relationship { get; set; }
    }
}
