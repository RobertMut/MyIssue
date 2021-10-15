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
                Id = 1,
                Name = "Locked"
            });
            builder.HasData(new UserType()
            {
                Id = 2,
                Name = "User"
            });
            builder.HasData(new UserType()
            {
                Id = 3,
                Name = "Admin"
            });
        }
    }
}