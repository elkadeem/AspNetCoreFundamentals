using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Fundamentals.WebApp.Areas.Identity.Services
{
    public class LoggerSmsSender : ISmsSender
    {
        private readonly ILogger<LoggerSmsSender> _logger;

        public LoggerSmsSender(ILogger<LoggerSmsSender> logger)
        {
            _logger = logger;
        }

        public Task SendSmsAsync(string message, string number)
        {
            _logger.LogInformation($"SMS For {number} with Subject {message}");
            return Task.CompletedTask;
        }
    }
}
