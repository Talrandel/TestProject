using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TestProject.Module.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args);
            host.Run();
        }
        

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:55485")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}