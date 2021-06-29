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
    public class CountriesController : Controller
    {
        private readonly IConfiguration _configuration;
        static readonly string apiUrl = "Country";

        public string BaseUrl
        {
            get
            {
                return _configuration["EndpointURLPCLAPI"];
            }
        }

        public CountriesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<CountryDTO> countryDTOs = new List<CountryDTO>();
                var requestUrl = $"{BaseUrl}{apiUrl}/GetCountries";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUrl);
                    if (responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        countryDTOs = await responseMessage.Content.ReadAsAsync<IEnumerable<CountryDTO>>();
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
                var requestUrl = $"{BaseUrl}{apiUrl}/GetTitleById?countryCode={id}";

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
    }
}
