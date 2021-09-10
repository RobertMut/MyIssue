using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyIssue.API.Model;

namespace MyIssue.API.Infrastructure.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(u => u.UserTypes)
                .WithMany(ut => ut.Users)
                .HasForeignKey(fk => fk.UserType);
            builder.HasData(new User()
            {
                UserLogin = "Admin",
                Password = "1234",
                UserType = 3
            });
        }
    }
}