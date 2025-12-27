using Kosha.CustomerManager.Web.Domain.Authenticate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosha.CustomerManager.Web.Persistence.Configurations.Authenticate;

internal sealed class RoleConfiguration : AuditConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.Property(model => model.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(model => model.Name)
            .IsUnique();

        builder.HasMany(model => model.Users)
            .WithOne(roleUser => roleUser.Role)
            .HasForeignKey(roleUser => roleUser.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}