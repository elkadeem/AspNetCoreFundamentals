using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Fundamentals.WebApp.Areas.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string message, string number);
    }
}
