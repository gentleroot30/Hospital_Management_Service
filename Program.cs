using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using NLog;
using System.Net.Http;
using System;

namespace HospitalMgmtService
{
    public class Program
    {
     
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Info("Starting application");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application stopped because of an exception");
                throw;
            }
            using var httpClient = new HttpClient();

         
            // Define custom headers for UserId and AccessToken
          //  httpClient.DefaultRequestHeaders.Add("UserId", "YourUserIdHere");
            //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer YourAccessTokenHere");

            // Make an HTTP request
            //var response =  httpClient.GetAsync("https://example.com");


        }

     
        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddNLog("nlog.config");
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });

    }
}
