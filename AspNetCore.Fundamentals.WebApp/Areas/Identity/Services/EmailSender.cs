using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Fundamentals.WebApp.Areas.Identity.Services
{
    public class LoggerEmailSender : IEmailSender
    {
        private readonly ILogger<LoggerEmailSender> _logger;

        public LoggerEmailSender(ILogger<LoggerEmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation($"Mail Message for {email} with Subject {subject} \n\r {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}
