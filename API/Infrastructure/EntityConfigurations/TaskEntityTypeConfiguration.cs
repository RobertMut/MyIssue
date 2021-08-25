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
                .HasOne(t => t.Employees)
                .WithMany(e => e.Tasks)
                .HasForeignKey(fk => fk.TaskOwner);
            builder
                .HasOne(t => t.Employees)
                .WithMany(e => e.Tasks)
                .HasForeignKey(fk => fk.TaskAssignment);
            builder
                .HasOne(t => t.TaskTypes)
                .WithMany(tt => tt.Tasks)
                .HasForeignKey(fk => fk.TaskType);
            builder
                .Property(t => t.TaskCreation)
                .HasColumnType("datetime2");
            builder
                .Property(t => t.TaskEnd)
                .HasColumnType("datetime2");
            builder
                .Property(t => t.TaskStart)
                .HasColumnType("datetime2");
            builder.Property(id => id.TaskId).HasPrecision(10, 0);
        }
    }
}