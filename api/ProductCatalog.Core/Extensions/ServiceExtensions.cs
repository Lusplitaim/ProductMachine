using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Extensions.Logging;
using ProductCatalog.Core.Managers;
using ProductCatalog.Core.Services;
using ProductCatalog.Core.Storages;

namespace ProductCatalog.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
        {
            ConfigureLogging(config);

            services.AddScoped<IProductBrandService, ProductBrandService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IProductBrandStorage, ProductBrandStorage>();
            services.AddScoped<IProductStorage, ProductStorage>();
            services.AddScoped<IOrderStorage, OrderStorage>();
            services.AddScoped<ICoinStorage, CoinStorage>();

            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddAuthHandlers();

            return services;
        }

        private static IServiceCollection AddAuthHandlers(this IServiceCollection services)
        {
            return services;
        }

        private static void ConfigureLogging(IConfiguration config)
        {
            LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
        }
    }
}
