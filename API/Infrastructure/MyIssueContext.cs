using Microsoft.EntityFrameworkCore;
using MyIssue.API.Infrastructure.EntityConfigurations;
using MyIssue.API.Model;

namespace MyIssue.API.Infrastructure
{
    public class MyIssueContext : DbContext
    {

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ClientEmployee> ClientEmployees { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Model.Task> Tasks { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public MyIssueContext(DbContextOptions<MyIssueContext> options) : base(options)
        {
        }
        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientEmployee>()
                .HasOne(ce => ce.Clients)
                .WithMany(c => c.ClientEmployees)
                .HasForeignKey(fk => fk.Client);
            modelBuilder.Entity<Employee>()
                .HasOne<Position>(e => e.Positions)
                .WithMany(e => e.Employees)
                .HasForeignKey(fk => fk.EmployeePosition);

            modelBuilder.Entity<Position>().Property(p => p.PositionId).HasPrecision(3, 0);
            modelBuilder.Entity<UserType>().Property(ut => ut.Id).HasPrecision(3, 0);
            modelBuilder.Entity<TaskType>().Property(t => t.TypeId).HasPrecision(4, 0);
            modelBuilder.Entity<ClientEmployee>().Property(id => id.EmployeeId).HasPrecision(10, 0);
            modelBuilder.Entity<Client>().Property(id => id.ClientId).HasPrecision(6, 0);

            modelBuilder.ApplyConfiguration(new TaskEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
        #endregion
    }
}