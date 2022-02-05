using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using TranzactProgrammingChallengeMVC.Models;

namespace TranzactProgrammingChallengeMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /* public HomeController(ILogger<HomeController> logger)
         {
             _logger = logger;
         }*/

        private readonly IConfiguration _config;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _config = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {

            HomeViewModel m = new HomeViewModel();

            // string baseAddress = "https://localhost:44346/";
            //Information from the appsettings.json
            string baseAddress = _config["WebAPIConfig:WebAddress"];

            ViewBag.BaseAddress = baseAddress;

            // Add values to the front end
            m.DateOfBirth = DateTime.Now;

            //Connect to the service in order to get information from the Web API
            using (var client = new HttpClient())
            {


                string baseStates = "StateList";
                string basePlan = "PlanList";
                string basePeriod = "PeriodList";
                client.BaseAddress = new Uri(baseAddress);

                m.States = new SelectList(GetListFromWebApi(client, baseStates, "stateId", "stateName"), "stateId", "stateName");
                m.Plans = new SelectList(GetListFromWebApi(client, basePlan, "planId", "planName"), "planId", "planName");
                m.Periods = new SelectList(GetListFromWebApi(client, basePeriod, "factor", "namePeriod"), "factor", "namePeriod");
            }
            return View(m);
        }

        /// <summary>
        /// This Function allows to connect and manage the conection to the service, if there is not connection: just send a valid void response to aviod errors on the web application
        /// </summary>
        /// <param name="client"></param>
        /// <param name="serviceName"></param>
        /// <param name="voidid"></param>
        /// <param name="voidvalue"></param>
        /// <returns></returns>
        public Newtonsoft.Json.Linq.JArray GetListFromWebApi(HttpClient client, string serviceName, string voidid, string voidvalue)
        {
            Newtonsoft.Json.Linq.JArray o;
            string voidResponse = "[{\"" + voidid + "\":\" \",\"" + voidvalue + "\":\" \"}]";
            try
            {
                var responseTask = client.GetAsync(serviceName);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    string strResult = readTask.Result;
                    o = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(strResult);

                    return o;
                }
                else
                {
                    _logger.LogInformation("WebAPI unable to respond");
                    o = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(voidResponse);
                    return o;

                }
            }
            catch
            {
                _logger.LogInformation("WebAPI unable to respond");
                o = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(voidResponse);
                return o;
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}