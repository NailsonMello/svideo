using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SVideo.API.AutoMapper;
using SVideo.API.Configurations;
using SVideo.API.Configurations.Converters;
using SVideo.API.Configurations.Filters;
using SVideo.API.Models.Responses;
using SVideo.Application.Resources;
using SVideo.Application.Validations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace SVideo.API
{
    /// <summary>
    /// Project Startup Class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="configuration">Config</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Config property
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services">Services Collection</param> 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionHandlerFilterAttribute));
                options.Filters.Add(typeof(ValidateModelStateFilterAttribute));
            })
            .AddFluentValidation(fvc =>
            {
                fvc.RegisterValidatorsFromAssemblyContaining<ServerValidator>();
                //fvc.RegisterValidatorsFromAssemblyContaining<ServerUpdateValidator>();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = c =>
                {
                    Resource resource = c.HttpContext.RequestServices.GetRequiredService<Resource>();
                    string errorMessage = resource.GetMessage("InvalidJson");

                    return new BadRequestObjectResult(new ServerErrorResponse
                                (
                                    errorMessage,
                                    null,
                                    null
                                ));
                };
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new EmptyStringJsonConverter()));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SVideo.Api", Version = "v1" });

                // Add Xml coments
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });

            services.AddWebEncoders(o =>
            {
                o.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(UnicodeRanges.All);
            });

            // Add DB Context
            services.AddCustomDataContext(Configuration);

            // Add Dependecy Injection
            services.AddDependencyInjection(Configuration);

            // Add AutoMapper
            services.AddAutoMapperSetup();

            // Localization
            services.AddLocalization(o => o.ResourcesPath = "");
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    string[] cultures = new string[]
                    {
                        "pt-BR",
                        "en-US",
                    };

                    options.SetDefaultCulture("pt-BR");
                    options.AddSupportedCultures(cultures);
                    options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                    {
                        return Task.FromResult(new ProviderCultureResult("pt-BR"));
                    }));
                });

            services.AddSingleton<Resource>();

            services.AddHealthCheckConfig(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <param name="app">Application builder</param>        
        /// <param name="env">Environments</param>   
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SVideo.Api v1"));

            app.UseCors(options => options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
            );

            app.UseHttpsRedirection();

            IOptions<RequestLocalizationOptions> localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizeOptions.Value);

            app.AddStaticFiles(Configuration);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.UseHealthCheckEndpoints();
            });
        }
    }
}
