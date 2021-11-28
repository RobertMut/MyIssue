using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyIssue.Main.API.Model;

namespace MyIssue.Main.API.Infrastructure.EntityConfigurations
{
    public class TaskTypeEntityTypeConfiguration : IEntityTypeConfiguration<TaskType>
    {
        public void Configure(EntityTypeBuilder<TaskType> builder)
        {
            builder.HasData(new TaskType()
            {
                TypeId = 1,
                TypeName = "Low priority"
            });
            builder.HasData(new TaskType()
            {
                TypeId = 2,
                TypeName = "Normal"
            });
            builder.HasData(new TaskType()
            {
                TypeId = 3,
                TypeName = "Urgent"
            });
        }
    }
}