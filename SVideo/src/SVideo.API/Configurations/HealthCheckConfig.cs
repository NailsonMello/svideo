using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NLog.Web;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;

namespace SVideo.API.Configurations
{
    /// <summary>
    /// Extension to configure Health check on startup
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class HealthCheckConfig
    {
        /// <summary>
        /// Method to config health check in Startup
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void AddHealthCheckConfig(this IServiceCollection services, IConfiguration config)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            string sVideoConnection = config["SVideoConnectionString"];

            logger.Info($"SVideo Connection: {sVideoConnection}");

            services.AddHealthChecks()
                .AddSqlServer(sVideoConnection, name: "svideo_db", timeout: TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Method to apply health check routes in Startup
        /// </summary>
        /// <param name="endpoints"></param>
        public static void UseHealthCheckEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health", GetJsonReturn());
        }

        private static HealthCheckOptions GetJsonReturn()
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            return new HealthCheckOptions()
            {
                Predicate = (check) => true,
                ResponseWriter = async (context, report) =>
                {
                    var healthCheck = report.Entries.Select(e => new
                    {
                        HealthCheck = e.Key,
                        Status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                        e.Value.Duration,
                        Exception = e.Value.Exception?.Message ?? e.Value.Exception?.InnerException?.Message,
                        e.Value.Description,
                        e.Value.Tags
                    }).ToList();

                    logger.Info($"HealthCheck: {healthCheck}");

                    var result = JsonSerializer.Serialize(healthCheck);

                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    await context.Response.WriteAsync(result);
                }
            };
        }
    }
}
