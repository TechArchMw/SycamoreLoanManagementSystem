
using SycamoreWebApp.DTOs.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SycamoreWebApp.DTOs
{
    public class AuthenticationDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public OutputHandler OutputHandler {get;set;}
    }

}
