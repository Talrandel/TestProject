using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using TestProject.Application.Movies;
using TestProject.Application.Persons;
using TestProject.Common.DAL.Core;
using TestProject.Common.DAL.MongoDB;
using TestProject.Common.Entities;
using TestProject.Domain.Movies;
using TestProject.Domain.Persons;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;

namespace TestProject.Module.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // options => options.EnableEndpointRouting = false
            //services.AddOptions();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",
            //        Title = "Test API",
            //        Description = "ASP.NET Core Web API"
            //    });
            //});
            //services.AddSingleton<IDbContext<Person, IdInt>, InMemoryDbContext<Person, IdInt>>();
            //services.AddSingleton<IMovieRepository, MovieRepository>();
            //services.AddSingleton<IPersonRepository, PersonRepository>();
            //ConfigureMongoDbServices(services);
            //ConfigureCustomServices(services);
        }
        private void ConfigureCustomServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        private void ConfigureMongoDbServices(IServiceCollection services)
        {
            var connectionString = ConfigurationExtensions
                .GetConnectionString(Configuration, "MongoDbConnection");
            var mongoClient = new MongoClient(connectionString);
            services.AddSingleton(provider => {
                return mongoClient;
            });
            var mongoDbParametersSection = Configuration.GetSection("MongoDbParameters");
            var parameters = mongoDbParametersSection.Get<MongoDbParameterContainer>();
            var databaseName = parameters.DatabaseName;
            // TODO: костыль!
            services.AddTransient(provider => {
                return new MongoDbContext<Movie, IdInt>(mongoClient, databaseName, parameters.Collections[0]);
            });
            services.AddTransient(provider => {
                return new MongoDbContext<Person, IdInt>(mongoClient, databaseName, parameters.Collections[1]);
            });
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseHsts();
            //}

            //app.UseSwagger();
            //app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1"));

            //app.UseHttpsRedirection();
            //app.UseMvc();
            app.UseMvc(ConfigureRoute);
        }

        private void ConfigureRoute(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller}/{action}");
        }
    }
}