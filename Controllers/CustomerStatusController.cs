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
    public class CustomerStatusController : Controller
    {
        private readonly IConfiguration _configuration;
        static readonly string apiUrl = "CustomerStatus";

        public string BaseUrl
        {
            get
            {
                return _configuration["EndpointURLPCLAPI"];
            }
        }

        public CustomerStatusController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<CustomerStatusDTO> countryDTOs = new List<CustomerStatusDTO>();
                var requestUrl = $"{BaseUrl}{apiUrl}/GetCustomerStatuses";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUrl);
                    if (responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        countryDTOs = await responseMessage.Content.ReadAsAsync<IEnumerable<CustomerStatusDTO>>();
                    }
                }
                return View(countryDTOs);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var outputHandler = new OutputHandler();
                var countryDTO = new CountryDTO();
                var requestUrl = $"{BaseUrl}{apiUrl}/GetCustomerStatusById?statusCode={id}";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUrl);
                    if (responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        outputHandler = await responseMessage.Content.ReadAsAsync<OutputHandler>();
                        countryDTO = (CountryDTO)outputHandler.Result;
                        return View(countryDTO);
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

        public async Task<IActionResult> Update(string id)
        {
            try
            {
                var outputHandler = new OutputHandler();
                var customerStatusDTO = new CustomerStatusDTO();
                var requestUrl = $"{BaseUrl}{apiUrl}/GetCustomerStatusById?id={id}";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUrl);
                    if (responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        outputHandler = await responseMessage.Content.ReadAsAsync<OutputHandler>();
                        var code = outputHandler.Result;
                        customerStatusDTO = (CustomerStatusDTO)outputHandler.Result;
                        return View(customerStatusDTO);
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
        public async Task<IActionResult> Update(CustomerStatusDTO customerStatusDTO, string id)
        {
            try
            {
                if (id != customerStatusDTO.StatusCode)
                {
                    return RedirectToAction("Error", "Home");
                }

                var outputHandler = new OutputHandler();
                var requestUrl = $"{BaseUrl}{apiUrl}/UpdateCustomerStatus";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    customerStatusDTO.DateModified = DateTime.UtcNow.AddHours(2);
                    customerStatusDTO.ModifiedBy = "SYSADMIN_MOD";
                    HttpResponseMessage responseMessage = await httpClient.PutAsJsonAsync(requestUrl, customerStatusDTO);
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
