using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using TestProject.Application.Movies.Services;
using TestProject.Application.Persons.Services;
using TestProject.Common.DAL.MongoDB;
using TestProject.Common.Entities;
using TestProject.Domain.Movies;
using TestProject.Domain.Persons;

namespace TestProject.WebApi
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Debug()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} " + "{Properties:j}{NewLine}{Exception}")
                .CreateLogger();

            var host = CreateWebHost(args);
#if DEBUG
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // TODO: #if MONGODB
                    var personContext = services.GetRequiredService(typeof(MongoDbContext<Person, IdInt>));
                    var movieContext = services.GetRequiredService(typeof(MongoDbContext<Movie, IdInt>));

                    var personSeedService = services.GetRequiredService<PersonSeedService>();
                    var movieSeedService = services.GetRequiredService<MovieSeedService>();

                    await personSeedService.Clear();
                    await personSeedService.Initialize();

                    await movieSeedService.Clear();
                    await movieSeedService.Initialize();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Во время инициализации базы данных произошла ошибка.");
                }
            }
#endif

            try
            {
                Log.Information("Запуск приложения.");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Критическая ошибка.");
                return;
            }
            finally
            {
                Log.Information("Завершение работы приложения.");
                Log.CloseAndFlush();
            }
        }

        public static IWebHost CreateWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(Configuration)
                .UseSerilog()
                .Build();
    }
}