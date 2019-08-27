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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddRouting(options => options.LowercaseUrls = true);
            //services.AddApiVersioning(config =>
            //{
            //    config.ReportApiVersions = true;
            //    config.AssumeDefaultVersionWhenUnspecified = true;
            //    config.DefaultApiVersion = new ApiVersion(1, 0);
            //    config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            //});
            //services.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "ASP.NET Core Web API"
                });
            });
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

        public void Configure(IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IHostingEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                //app.UseExceptionHandler(errorApp =>
                //{
                //    errorApp.Run(async context =>
                //    {
                //        context.Response.StatusCode = 500;
                //        context.Response.ContentType = "text/plain";
                //        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                //        if (errorFeature != null)
                //        {
                //            var logger = loggerFactory.CreateLogger("Global exception logger");
                //            logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);
                //        }

                //        await context.Response.WriteAsync("There was an error");
                //    });
                //});
            }

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1"));

            app.UseHttpsRedirection();
            app.UseMvc(ConfigureRoute);
        }

        private void ConfigureRoute(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller}/{action}");
        }
    }
}