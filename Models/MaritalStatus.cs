﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SycamoreWebApp.Models
{
    public partial class MaritalStatus
    {
        public int MaritalStatusId { get; set; }
        public string MaritalStatusDescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
    }
}
