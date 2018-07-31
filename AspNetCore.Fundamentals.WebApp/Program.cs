using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace AspNetCore.Fundamentals.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder
                .ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("Initiate Main");
                CreateWebHostBuilder(args)
                .Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)                
                .UseStartup<Startup>()
                .ConfigureLogging((logging) => {
                    logging.ClearProviders(); // Clear other providers
                    logging.SetMinimumLevel(LogLevel.Trace);                    
                })
                .UseNLog();
    }
}
