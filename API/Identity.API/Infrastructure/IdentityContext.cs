using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using MyIssue.Identity.API.Infrastructure.EntityConfigurations;
using MyIssue.Identity.API.Model;

namespace MyIssue.Identity.API.Infrastructure
{
    public class IdentityContext : DbContext
    {

        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }
        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserType>().Property(ut => ut.Id).HasPrecision(3, 0);
            
            modelBuilder.ApplyConfiguration(new UserTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
        #endregion
    }
}