using Kosha.CustomerManager.Web.Domain.Authenticate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosha.CustomerManager.Web.Persistence.Configurations.Authenticate;

internal sealed class UserConfiguration  : AuditConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(model => model.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(model => model.Username)
            .IsUnique();

        builder.Property(model => model.Password)
            .IsRequired();

        builder.Property(model => model.Name)
            .IsRequired(false);

        builder.Property(model => model.Family)
            .IsRequired(false);

        builder.HasMany(model => model.Roles)
            .WithOne(roleUser => roleUser.User)
            .HasForeignKey(roleUser => roleUser.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(model => model.Actions)
            .WithOne(model => model.User)
            .HasForeignKey(model => model.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}