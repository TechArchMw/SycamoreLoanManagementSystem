using System;
using System.Collections.Generic;

#nullable disable

namespace SycamoreWebApp.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Loans = new HashSet<Loan>();
        }

        public int CustomerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int? Title { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}
