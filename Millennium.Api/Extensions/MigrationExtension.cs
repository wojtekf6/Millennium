using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Millennium.Data;

namespace Millennium.Api.Extensions
{
    public static class MigrationExtension
    {
        public static IApplicationBuilder MigrateDb(this IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            var context = serviceScope?.ServiceProvider.GetRequiredService<Context>();
            context?.Database.Migrate();

            return applicationBuilder;
        }
    }
}