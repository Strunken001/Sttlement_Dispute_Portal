using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using src.Controllers;
using src.Data;
using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace src.Services
{
    public class ADLogin : IADLogin
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private IConfiguration configuration;
        private string ADbaseUrl;

        //dependency injection
        public ADLogin(              
                IConfiguration _configuration,
                ILogger<AccountController> logger,
                ApplicationDbContext context)
        {
            
            _logger = logger;
            _context = context;
            configuration = _configuration;
            ADbaseUrl = configuration.GetValue<string>("ADBaseUrl");
        }

        public bool verifyADUser(string username, string password, string key)
        {
            bool status = false;
            //var str = "{ "Username": "username", "upper_bound": "password" }"
                string str = "{ \"Username\": " + username + " \"Password\": " + password + "  \"AppKey\": " + key + "}";
            try
            {
                //Validate CoralPay User
                //Reservation receivedReservation = new Reservation();
                using (var httpClient = new HttpClient())
                {
                    
                    StringContent content = new StringContent(JsonConvert.SerializeObject(str), Encoding.UTF8, "application/json");

                    using (var response = httpClient.PostAsync(ADbaseUrl, content).Result)
                    {
                        _logger.LogInformation("Response from AD is: " + response.Content);
                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        //string apiResponse = await response.Content.ReadAsStringAsync();
                        //receivedReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                    }
                }

            }
            catch (Exception mm)
            {
                _logger.LogError("An error has occured whie validating AD user: " + mm.Message + mm.StackTrace);
                return status;
            }
            return status;
        }
    }
}
