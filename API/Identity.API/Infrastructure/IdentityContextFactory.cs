using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MyIssue.Identity.API.Infrastructure
{
    public class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            optionsBuilder.UseSqlServer(config["ConnectionString"],
                sqlServerOptionsAction: o => o.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            return new IdentityContext(optionsBuilder.Options);
        }
    }
}