using System;

namespace SycamoreWebApp.Controllers
{
    public class PersonalDetail
    {
        public string RegNo { get; internal set; }
        public string Name { get; internal set; }
        public string Address { get; internal set; }
        public string PhoneNo { get; internal set; }
        public DateTime AdmissionDate { get; internal set; }
    }
}