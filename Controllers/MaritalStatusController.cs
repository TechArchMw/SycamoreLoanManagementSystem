using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SycamoreWebApp.DTOs;
using SycamoreWebApp.DTOs.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SycamoreWebApp.Controllers
{
    public class MaritalStatusController : Controller
    {
        private readonly IConfiguration _configuration;
        static readonly string apiUrl = "MaritalStatuses";

        public string BaseUrl
        {
            get
            {
                return _configuration["EndpointURLPCLAPI"];
            }
        }

        public MaritalStatusController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<MaritalStatusDTO> maritalStatuses = new List<MaritalStatusDTO>();
                var requestUrl = $"{BaseUrl}{apiUrl}/GetMaritalStatuses";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUrl);
                    if (responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        maritalStatuses = await responseMessage.Content.ReadAsAsync<IEnumerable<MaritalStatusDTO>>();
                    }
                }
                return View(maritalStatuses);
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
        public async Task<IActionResult> Create(MaritalStatusDTO MaritalStatusDTO)
        {
            try
            {
                var outputHandler = new OutputHandler();
                var requestUrl = $"{BaseUrl}{apiUrl}/AddMaritalStatus";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    MaritalStatusDTO.DateCreated = DateTime.UtcNow.AddHours(2);
                    MaritalStatusDTO.DateModified = DateTime.UtcNow.AddHours(2);
                    MaritalStatusDTO.CreatedBy = "SYSADMIN";
                    HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(requestUrl, MaritalStatusDTO);
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
                var MaritalStatusDTO = new MaritalStatusDTO();
                var requestUrl = $"{BaseUrl}{apiUrl}/GetMaritalStatusById?id={id}";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUrl);
                    if (responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        outputHandler = await responseMessage.Content.ReadAsAsync<OutputHandler>();
                        MaritalStatusDTO = (MaritalStatusDTO)outputHandler.Result;
                        return View(MaritalStatusDTO);
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
        public async Task<IActionResult> Update(MaritalStatusDTO MaritalStatusDTO, int id)
        {
            try
            {
                if (id != MaritalStatusDTO.MaritalStatusId)
                {
                    return RedirectToAction("Error", "Home");
                }

                var outputHandler = new OutputHandler();
                var requestUrl = $"{BaseUrl}{apiUrl}/UpdateMaritalStatus";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    MaritalStatusDTO.DateModified = DateTime.UtcNow.AddHours(2);
                    MaritalStatusDTO.ModifiedBy = "SYSADMIN_MOD";
                    HttpResponseMessage responseMessage = await httpClient.PutAsJsonAsync(requestUrl, MaritalStatusDTO);
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

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var outputHandler = new OutputHandler();
                var loanTypeDTO = new LoanTypeDTO();
                var requestUrl = $"{BaseUrl}{apiUrl}/GetMaritalStatusById?id={id}";

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
    }
}
