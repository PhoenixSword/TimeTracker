using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Google.Cloud.Firestore;
using System;

namespace TimeTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string credential_path = @"./timetracker-5c762-d0e501de9a71.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
