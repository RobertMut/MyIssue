using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyIssue.API.Model;

namespace MyIssue.API.Infrastructure.EntityConfigurations
{
    public class TaskEntityTypeConfiguration : IEntityTypeConfiguration<Model.Task>
    {
        public void Configure(EntityTypeBuilder<Model.Task> builder)
        {
            builder
                .HasOne(t => t.Clients)
                .WithMany(c => c.Tasks)
                .HasForeignKey(fk => fk.TaskClient);
            builder
                .HasOne(t => t.EmployeesOwnership)
                .WithMany()
                .HasForeignKey(fk => fk.TaskOwner)
                .IsRequired(false);
            builder
                .HasOne(t => t.EmployeesAssignment)
                .WithMany()
                .HasForeignKey(fk => fk.TaskAssignment)
                .IsRequired(false);
            builder
                .HasOne(t => t.TaskTypes)
                .WithMany(tt => tt.Tasks)
                .HasForeignKey(fk => fk.TaskType);
            builder
                .Property(t => t.TaskCreation)
                .HasColumnType("datetime2");
            builder
                .Property(t => t.TaskEnd)
                .HasColumnType("datetime2")
                .IsRequired(false);
            builder
                .Property(t => t.TaskStart)
                .HasColumnType("datetime2")
                .IsRequired(false);
            builder.Property(id => id.TaskId).HasPrecision(10, 0);
        }
    }
}