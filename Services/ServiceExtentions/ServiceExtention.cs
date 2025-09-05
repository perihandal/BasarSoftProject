using App.Core.Configuration;
using App.Core.Validators;
using App.Services.Interfaces;
using App.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.ServiceExtentions
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WktValidationConfig>(options =>
            configuration.GetSection("WktValidation").Bind(options));

            services.AddScoped<GeometryValidator>();
            services.AddScoped<IGeometryInfoService,GeometryInfoService>();
            services.AddScoped<IGeometryMetricsService, GeometryMetricsService>();
            services.AddScoped<IGeometryService, GeometryService>();
            return services;
        }
    }
}
