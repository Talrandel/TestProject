using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using TestProject.Application.Movies;
using TestProject.Application.Persons;
using TestProject.Common.DAL.Core;
using TestProject.Common.DAL.MongoDB;
using TestProject.Common.Entities;
using TestProject.Domain.Movies;
using TestProject.Domain.Persons;

namespace TestProject.Module.WebApi2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "ASP.NET Core Web API"
                });
            });
            ConfigureCustomServices(services);
        }

        private void ConfigureCustomServices(IServiceCollection services)
        {
            ConfigureMongoDbServices(services);
        }

        private void ConfigureMongoDbServices(IServiceCollection services)
        {
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoDbSettings:ConnectionString").Value;
                options.DatabaseName = Configuration.GetSection("MongoDbSettings:DatabaseName").Value;
            });

            services.AddTransient<IDbContext<Movie, IdInt>, MongoDbContext<Movie, IdInt>>();
            services.AddTransient<IDbContext<Person, IdInt>, MongoDbContext<Person, IdInt>>();

            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1"));

            app.UseMvc();
        }

        private void ConfigureRoute(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller}/{action}");
        }
    }
}