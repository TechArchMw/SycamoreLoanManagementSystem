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
    public class TitleController : Controller
    {
        private readonly IConfiguration _configuration;
        static readonly string apiUrl = "Title";

        public string BaseUrl
        {
            get
            {
                return _configuration["EndpointURLPCLAPI"];
            }
        }

        public TitleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<TitleDTO> TitleDTOs = new List<TitleDTO>();
                var requestUrl = $"{BaseUrl}{apiUrl}/GetTitles";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUrl);
                    if (responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        TitleDTOs = await responseMessage.Content.ReadAsAsync<IEnumerable<TitleDTO>>();
                    }
                }
                return View(TitleDTOs);
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
        public async Task<IActionResult> Create(TitleDTO TitleDTO)
        {
            try
            {
                var outputHandler = new OutputHandler();
                var requestUrl = $"{BaseUrl}{apiUrl}/AddTitle";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    TitleDTO.DateCreated = DateTime.UtcNow.AddHours(2);
                    TitleDTO.DateModified = DateTime.UtcNow.AddHours(2);
                    TitleDTO.CreatedBy = "SYSADMIN";
                    HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(requestUrl, TitleDTO);
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

        public async Task<IActionResult> Update(string id)
        {
            try
            {
                var outputHandler = new OutputHandler();
                var TitleDTO = new TitleDTO();
                var requestUrl = $"{BaseUrl}{apiUrl}/GetTitleById?titleCode={id}";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(requestUrl);
                    if (responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        outputHandler = await responseMessage.Content.ReadAsAsync<OutputHandler>();
                        TitleDTO = (TitleDTO)outputHandler.Result;
                        return View(TitleDTO);
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
        public async Task<IActionResult> Update(TitleDTO TitleDTO, string id)
        {
            try
            {
                if (id != TitleDTO.TitleCode)
                {
                    return RedirectToAction("Error", "Home");
                }

                var outputHandler = new OutputHandler();
                var requestUrl = $"{BaseUrl}{apiUrl}/UpdateTitle";

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(requestUrl);
                    TitleDTO.DateModified = DateTime.UtcNow.AddHours(2);
                    TitleDTO.ModifiedBy = "SYSADMIN_MOD";
                    HttpResponseMessage responseMessage = await httpClient.PutAsJsonAsync(requestUrl, TitleDTO);
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
                var requestUrl = $"{BaseUrl}{apiUrl}/GetTitleById?titleCode={id}";

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
