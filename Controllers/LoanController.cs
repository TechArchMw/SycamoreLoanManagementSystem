using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SycamoreWebApp.DTOs;
using SycamoreWebApp.DTOs.General;
using SycamoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechArchDataHandler.AutoMapper;



namespace SycamoreWebApp.Controllers

{
    public class LoanController : Controller
    {
        private const string Token = "Token";
        private readonly IConfiguration _configuration;
        private readonly SycamoreDBContext _context;
        public LoanController(SycamoreDBContext context)
        {
            _context = context;
        }
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
            var loan = new LoanDTO
            {
                OutputHandler = new OutputHandler { IsErrorOccured = false }
            };
            return View(loan);
        }

        [HttpPost]
        public IActionResult Create(LoanDTO loan)
        {
          

            if (loan.NetSalaryAsPerCurrentPaySlip < loan.Amount)
            {
                var output = new OutputHandler
                {
                    IsErrorKnown = true,
                    IsErrorOccured = true,
                    Message = "Loan Amount cannot be higher than Current Net Pay"
                };

                //just one check for demostration purposes
                loan.OutputHandler = output;
                return View(loan);
            }
            loan.DateCreated = DateTime.UtcNow.AddHours(2);
            loan.CreatedBy = "Sys";
            loan.LoanTypeId = 1; //the only available loanType in the DB = Mizu 
            loan.loanStatusId = 7; //Set status to submit
            if (loan.Name == "Micheal Banda")
            {
                loan.CustomerId = 1;
            }
            else if (loan.Name == "John Chisale")
            {
                loan.CustomerId = 2;
            }
            else if (loan.Name == "Mateyu Biswick")
            {
                loan.CustomerId = 3;
            }
            else
            {

                loan.CustomerId = 4;
            }
            try
            {
                var mapped = new AutoMapper<LoanDTO, Loan>().MapToObject(loan);
                _context.Loans.Add(mapped);
                _context.SaveChanges();
                return RedirectToAction("Index", "Loan", new { result = "Loan Create" });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //ASSUMPTION
        //Loan should not be updated but can be rejected
        //rejected and closed loans can be recreated as completely new entries using the recreation feature
        //or used as a draft for new loans - discuss possible disadvantages 
        public IActionResult RecreateLoan(int loanId)
        {
            var loan = _context.Loans.Where(x => x.LoanId == loanId).FirstOrDefault();
            var mapped = new AutoMapper<Loan, LoanDTO>().MapToObject(loan);
            mapped.OutputHandler = new OutputHandler { IsErrorOccured = false };
            return View(mapped);
        }

        #region StatusChanges
        //Real scenario get userId and attach to action
        public IActionResult ReviewLoan(int loanId)
        {
            var loan = _context.Loans.Where(x => x.LoanId == loanId).FirstOrDefault();
            loan.LoanStatusId = 5; //rejected statuse
            _context.Loans.Update(loan);
            _context.SaveChanges();
            return RedirectToAction("Details", "Loan", new { message = "Loan Marked as Reviewed", id = loanId });
        }
        public IActionResult RejectLoan(int loanId)
        {
            var loan = _context.Loans.Where(x => x.LoanId == loanId).FirstOrDefault();
            loan.LoanStatusId = 3; //rejected statuse
            _context.Loans.Update(loan);
            _context.SaveChanges();
           return RedirectToAction("Details","Loan",new { message = "Loan Rejected Successfully", id = loanId });
        }
        public IActionResult CloseLoan(int loanId)
        {
            var loan = _context.Loans.Where(x => x.LoanId == loanId).FirstOrDefault();
            loan.LoanStatusId = 4; //Closed statuse
            _context.Loans.Update(loan);
            _context.SaveChanges();
            return RedirectToAction("Details", "Loan", new { message = "Loan Closed Successfully", id=loanId });
        }
        public IActionResult ApproveLoan(int loanId)
        {
            var loan = _context.Loans.Where(x => x.LoanId == loanId).FirstOrDefault();
            loan.LoanStatusId = 3; //rejected statuse
            _context.Loans.Update(loan);
            _context.SaveChanges();
            return RedirectToAction("Details", "Loan", new { message = "Loan Aprroved Successfully", id = loanId });
        }
  #endregion
        public IActionResult Index(string result = "")
        {
            var loan = _context.Loans.ToList();
            var mapped = new AutoMapper<Loan, LoanDTO>().MapToList(loan);
            foreach (var item in mapped)
            {
                var loanType = (from loanTypes in _context.LoanTypes
                                where loanTypes.LoanTypeId == item.LoanTypeId
                                select new
                                LoanTypeDTO
                                {
                                    LoanDescription = loanTypes.LoanDescription,
                                    AdminFee = loanTypes.AdminFee,
                                    Interest = loanTypes.Interest
                                }).FirstOrDefault();
                var customer = _context.Customers.Where(x => x.CustomerId == item.CustomerId).Select(x => x.Firstname + " " + x.Lastname).FirstOrDefault();
                var loanStatus = _context.LoanStatuses.Where(x => x.LoanStatusesId == item.loanStatusId).Select(x => x.StatusDescription).FirstOrDefault();
                item.LoanTypeDTO = loanType ;
                item.CustomerName = customer;
                item.LoanStatus = loanStatus;
            }
            return View(mapped);
        }

        public IActionResult Details(int id, string message ="")
        {
            var loan = _context.Loans.Where(x => x.LoanId == id).FirstOrDefault();
            var loanType = _context.LoanTypes.Where(x => x.LoanTypeId == loan.LoanTypeId).Select(x => x.LoanDescription).FirstOrDefault();
            var status = _context.LoanStatuses.Where(x => x.LoanStatusesId == loan.LoanStatusId).Select(x => x.StatusDescription).FirstOrDefault();
            var customerName = _context.Customers.Where(x => x.CustomerId == loan.CustomerId).Select(x => x.Firstname +" "+ x.Lastname).FirstOrDefault();
            var mapped = new AutoMapper<Loan, LoanDTO>().MapToObject(loan);
            mapped.LoanTypeDTO = new LoanTypeDTO {LoanDescription = loanType } ;
            mapped.LoanStatus = status;
            mapped.CustomerName = customerName;
            mapped.OutputHandler = new OutputHandler { IsErrorOccured = false,Message = message };
            return View(mapped);

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
