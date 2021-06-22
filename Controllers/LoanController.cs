using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SycamoreWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechArchDataHandler.General;


namespace SycamoreWebApp.Controllers

{
    public class LoanController : Controller
    {
        private const string Token = "Token";
        private readonly IConfiguration _configuration;

        static string apiUri = "Utils";
        public string BaseUrl
        {
            get
            {
                return _configuration["EndpointURL"];
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        //ASSUMPTION
        //Loan should not be updated but can be rejected
        //rejected and closed loans can be recreated as completely new entries using the recreation feature
        //or used as a draft for new loans - discuss possible disadvantages 
        public IActionResult RecreateLoan()
        {
            return View();
        }

        public IActionResult ReviewApproval()
        {
            return View();
        }

        public IActionResult Approval()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CalculateLoan()
        {
            return RedirectToAction("Create");
        }

        [HttpGet]
        public IEnumerable<LoanRepaymentScheduleDTO> LoanRepaymentSchedule(LoanDTO loan)
        {
            List<LoanRepaymentScheduleDTO> students = new List<LoanRepaymentScheduleDTO>
           {
              new LoanRepaymentScheduleDTO
              {
                  Period = 0,
                  Installment = 0,
                  CapitalRepayment  = 424000,
                  CapitalBalance  = 0,
                  InterestPayment = 0,
                  AdminFee = 0,
                  CRIIA = 0,


                              },
           new LoanRepaymentScheduleDTO{

                  Period = 0,
                  Installment = 128000,
                  CapitalRepayment  = 930000,
                  CapitalBalance  = 19080,
                  InterestPayment = 300,
                  AdminFee = 131212,
                  CRIIA = 56151
                             },
           };
            return students;
        }


        //public async Task<IActionResult> Index()
        //{
        //    var requestUrl = $"{BaseUrl}{apiUri}/GetAllLoans";
        //    LoanVM loanVM = new LoanVM(_configuration);
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(requestUrl);
        //        HttpResponseMessage response = await client.GetAsync(requestUrl);
        //        OutputHandler outputHandler = new OutputHandler();
        //        if (response.StatusCode == HttpStatusCode.OK)
        //        {

        //            loanVM.OutputHandler = outputHandler;
        //            // loanVM.Loans = await response.Content.ReadAsAsync<IEnumerable<LoanDTO>>();
        //        };
        //        loanVM.PageSetup.PageTitle = "Testimonies";


        //    }
        //    return View(loanVM);
        //}
    }
}
