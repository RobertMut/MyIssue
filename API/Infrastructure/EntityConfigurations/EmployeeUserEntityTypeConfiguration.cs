using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyIssue.Main.API.Model;

namespace MyIssue.Main.API.Infrastructure.EntityConfigurations
{
    public class EmployeeUserEntityTypeConfiguration : IEntityTypeConfiguration<Model.EmployeeUser>
    {
        public void Configure(EntityTypeBuilder<EmployeeUser> builder)
        {
            builder
                .Property(u => u.EmployeeLogin)
                .IsRequired(false)
                .ValueGeneratedOnAdd();
            builder
                .Property(e => e.UserLogin)
                .IsRequired(false)
                .ValueGeneratedOnAdd();
            builder
                .HasKey(ue => new { ue.EmployeeLogin, ue.UserLogin });

            builder
                .HasIndex(ue => ue.EmployeeLogin).IsUnique();
            builder
                .HasIndex(ue => ue.UserLogin).IsUnique();

            builder
                .HasOne(u => u.Employee)
                .WithOne(o => o.EmployeeUser)
                .HasForeignKey<EmployeeUser>(f => f.EmployeeLogin);

            builder
                .HasOne(u => u.User)
                .WithOne(o => o.EmployeeUser)
                .HasForeignKey<EmployeeUser>(f => f.UserLogin);
        }
    }
}
