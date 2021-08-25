using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyIssue.API.Infrastructure
{
    public class MyIssueContextFactory : IDesignTimeDbContextFactory<MyIssueContext>
    {
        public MyIssueContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<MyIssueContext>();
            optionsBuilder.UseSqlServer(config["ConnectionString"],
                sqlServerOptionsAction: o => o.MigrationsAssembly(nameof(MyIssueContext)));
            return new MyIssueContext(optionsBuilder.Options);
        }
    }
}