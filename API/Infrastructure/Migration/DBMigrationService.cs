using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MyIssue.API.Infrastructure.Migration
{
    public static class DBMigrationService
    {
        public static IServiceCollection AddDBMigration<TContext>(this IServiceCollection service)
            where TContext : DbContext
        {
            var provider = service.BuildServiceProvider();
            var context = provider.GetRequiredService<TContext>();

            context.Database.Migrate();

            return service;
        }
    }
}