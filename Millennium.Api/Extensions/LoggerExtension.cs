using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Millennium.Api.Controllers;

namespace Millennium.Api.Extensions
{
    public static class LoggerExtension
    {
        public static void AddLogger(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<CarController>>();
            services.AddSingleton(typeof(ILogger), logger);
        }
    }
}