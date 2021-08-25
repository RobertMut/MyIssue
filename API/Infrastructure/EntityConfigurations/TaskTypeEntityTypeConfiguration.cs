using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyIssue.API.Model;

namespace MyIssue.API.Infrastructure.EntityConfigurations
{
    public class TaskTypeEntityTypeConfiguration : IEntityTypeConfiguration<TaskType>
    {
        public void Configure(EntityTypeBuilder<TaskType> builder)
        {
            builder.HasData(new TaskType()
            {
                TypeName = "Low priority"
            });
            builder.HasData(new TaskType()
            {
                TypeName = "Normal"
            });
            builder.HasData(new TaskType()
            {
                TypeName = "Urgent"
            });
        }
    }
}