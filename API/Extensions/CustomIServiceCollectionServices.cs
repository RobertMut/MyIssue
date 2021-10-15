using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MyIssue.API.Extensions
{
    public static class CustomIServiceCollectionServices
    {
        public static IServiceCollection AddDbMigration<TContext>(this IServiceCollection service)
            where TContext : DbContext
        {
            var serviceProvider = service.BuildServiceProvider();

            var dbContext = serviceProvider.GetRequiredService<TContext>();

            dbContext.Database.Migrate();
            return service;
        }
    }
}
