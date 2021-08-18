using System.Data.Entity;

namespace MyIssue.Infrastructure.Database.Models
{
    public class MyIssueContext : DbContext
    {

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ClientEmployee> ClientEmployees { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public MyIssueContext(string myIssueContext) : base(myIssueContext)
        {
        }
        #region Required
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Task>()
                .HasRequired(t => t.Clients)
                .WithMany()
                .HasForeignKey(fk => fk.TaskClient);
            modelBuilder.Entity<Task>()
                .HasOptional(t => t.Employees)
                .WithMany()
                .HasForeignKey(fk => fk.TaskOwner);
            modelBuilder.Entity<Task>()
                .HasOptional(t => t.Employees)
                .WithMany()
                .HasForeignKey(fk => fk.TaskAssignment);
            modelBuilder.Entity<Task>()
                .HasRequired(t => t.TaskTypes)
                .WithMany()
                .HasForeignKey(fk => fk.TaskType);
            modelBuilder.Entity<ClientEmployee>()
                .HasRequired(ce => ce.Clients)
                .WithMany()
                .HasForeignKey(fk => fk.Client);
            modelBuilder.Entity<Employee>()
                .HasRequired<Position>(e => e.Positions)
                .WithMany()
                .HasForeignKey(fk => fk.EmployeePosition);
            modelBuilder.Entity<User>()
                .HasRequired(u => u.UserTypes)
                .WithMany()
                .HasForeignKey(fk => fk.UserType);
            modelBuilder.Entity<User>()
                .HasOptional(u => u.Employee)
                .WithOptionalPrincipal(p => p.EmployeeLogins);

            modelBuilder.Entity<Task>().Property(id => id.TaskId).HasPrecision(10, 0);
            modelBuilder.Entity<Position>().Property(p => p.PositionId).HasPrecision(3, 0);
            modelBuilder.Entity<UserType>().Property(ut => ut.Id).HasPrecision(3, 0);
            modelBuilder.Entity<TaskType>().Property(t => t.TypeId).HasPrecision(4, 0);
            modelBuilder.Entity<ClientEmployee>().Property(id => id.EmployeeId).HasPrecision(10, 0);
            modelBuilder.Entity<Client>().Property(id => id.ClientId).HasPrecision(6, 0);
        }
        #endregion
    }
}