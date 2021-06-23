using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SycamoreWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System.Net.Http;
using SycamoreWebApp.DTOs.General;
using Audit.WebApi;

namespace SycamoreWebApplication.Controllers
{
    [AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    public class LoanTypeController : Controller
    {
        private readonly IConfiguration _configuration;
        static readonly string apiUrl = "LoanTypes";

        public string BaseUrl
        {
            get
            {
                return _configuration["EndpointURLPCLAPI"];
            }
        }

        public LoanTypeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<LoanTypeDTO> loanTypeDTOs = new List<LoanTypeDTO>();
                var requestUrl = $"{BaseUrl}{apiUrl}/GetLoanTypes";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUrl);
                    if (responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        loanTypeDTOs = await responseMessage.Content.ReadAsAsync<IEnumerable<LoanTypeDTO>>();
                    }
                }
                return View(loanTypeDTOs);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoanTypeDTO loanTypeDTO)
        {
            try
            {
                var outputHandler = new OutputHandler();
                var requestUrl = $"{BaseUrl}{apiUrl}/AddLoanType";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    loanTypeDTO.DateCreated = DateTime.UtcNow.AddHours(2);
                    loanTypeDTO.CreatedBy = "SYSADMIN";
                    HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(requestUrl, loanTypeDTO);
                    if (responseMessage.StatusCode == HttpStatusCode.Created)
                    {
                        outputHandler = await responseMessage.Content.ReadAsAsync<OutputHandler>();
                        return RedirectToAction(nameof(Index), new { outputHandler.Message });
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { outputHandler.Message });
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var outputHandler = new OutputHandler();
                var loanTypeDTO = new LoanTypeDTO();
                var requestUrl = $"{BaseUrl}{apiUrl}/GetLoanTypeById?id={id}";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUrl);
                    if (responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        outputHandler = await responseMessage.Content.ReadAsAsync<OutputHandler>();
                        loanTypeDTO = (LoanTypeDTO)outputHandler.Result;
                        return View(loanTypeDTO);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { outputHandler.Message });
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(LoanTypeDTO loanTypeDTO, int id)
        {
            try
            {
                if (id != loanTypeDTO.LoanTypeId)
                {
                    return RedirectToAction("Error", "Home");
                }

                var outputHandler = new OutputHandler();
                var requestUrl = $"{BaseUrl}{apiUrl}/UpdateLoanType";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    loanTypeDTO.DateModified = DateTime.UtcNow.AddHours(2);
                    loanTypeDTO.ModifiedBy = "SYSADMIN_MOD";
                    HttpResponseMessage responseMessage = await httpClient.PutAsJsonAsync(requestUrl, loanTypeDTO);
                    if (responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        outputHandler = await responseMessage.Content.ReadAsAsync<OutputHandler>();
                        return RedirectToAction(nameof(Index), new { outputHandler.Message });
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { outputHandler.Message });
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
