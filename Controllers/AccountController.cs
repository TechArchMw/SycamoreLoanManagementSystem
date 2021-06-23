using Microsoft.AspNetCore.Mvc;
using SycamoreWebApp.DTOs;
using SycamoreWebApp.DTOs.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SycamoreWebApp.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            AuthenticationDTO authentication = new AuthenticationDTO
            {
                OutputHandler = new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = "Login Failed: Wrong username or password."
                }
            };

            return View(authentication);
        }

        [HttpPost]
        public IActionResult Login(AuthenticationDTO authentication)
        {
            if (authentication.Username == "JPhiri" && authentication.Password == "phiri1")
            {
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                authentication.OutputHandler = new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = "Login Failed: Wrong username or password."
                };
                return View(authentication);
            }

        }
       
    }
}
