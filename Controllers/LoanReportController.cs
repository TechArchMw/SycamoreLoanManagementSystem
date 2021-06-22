using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SycamoreWebApp.Controllers
{
    public class LoanReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoanReport()
        {
            return View();
        }

        public IActionResult LoanProductsReport()
        {
            return View();
        }


    }
}
