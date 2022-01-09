using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using src.Controllers;
using src.Data;
using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace src.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private IConfiguration configuration;
        private string baseUrl;
        //dependency injection
        public SendGridOptions _sendGridOptions { get; }
        public IDotnetdesk _dotnetdesk { get; }
        public SmtpOptions _smtpOptions { get; }
        public EmailSender(IOptions<SendGridOptions> sendGridOptions,
                IDotnetdesk dotnetdesk,
                IConfiguration _configuration,
                ILogger<AccountController> logger,
                ApplicationDbContext context,
                IOptions<SmtpOptions> smtpOptions)
        {
            _sendGridOptions = sendGridOptions.Value;
            _dotnetdesk = dotnetdesk;
            _smtpOptions = smtpOptions.Value;
            _logger = logger;
            _context = context;
            configuration = _configuration;
            baseUrl = configuration.GetValue<string>("EmailBaseUrl");
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {

            string source = "itcomms@coralpay.com";

            using (var httpClient = new HttpClient())
            {
                var client = new RestClient("https://testdev.coralpay.com/CoralMailService/api/SendEmail");

                var request = new RestRequest(Method.POST);
                request.AddParameter("To", email);
                request.AddParameter("ToName", email);
                request.AddParameter("From", source);
                request.AddParameter("FromName", "CoralPay");
                request.AddParameter("Subject", subject);
                request.AddParameter("Body", message);

                IRestResponse response = client.Execute(request);
            }

            return Task.CompletedTask;
        }
    }
}
