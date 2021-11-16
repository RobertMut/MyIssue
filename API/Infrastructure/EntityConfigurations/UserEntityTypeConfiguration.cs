using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyIssue.Main.API.Model;

namespace MyIssue.Main.API.Infrastructure.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(u => u.UserTypes)
                .WithMany(ut => ut.Users)
                .HasForeignKey(fk => fk.UserType);

            // builder
            //     .HasOne(e => e.EmployeeUser)
            //     .WithOne()
            //     .HasForeignKey<EmployeeUser>(e => e.UserLogin);
            builder.HasData(new User()
            {
                UserLogin = "Admin",
                Password = "1234",
                UserType = 3
            });
        }
    }
}