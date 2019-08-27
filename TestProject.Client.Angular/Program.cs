using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MongoDBTest.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}