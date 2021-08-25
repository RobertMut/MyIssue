using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyIssue.API.Model;

namespace MyIssue.API.Infrastructure.EntityConfigurations
{
    public class UserTypeEntityTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.HasData(new UserType()
            {
                Name = "Locked"
            });
            builder.HasData(new UserType()
            {
                Name = "User"
            });
            builder.HasData(new UserType()
            {
                Name = "Admin"
            });
        }
    }
}