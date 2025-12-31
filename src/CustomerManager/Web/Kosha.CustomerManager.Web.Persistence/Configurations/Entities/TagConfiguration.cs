using Kosha.CustomerManager.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosha.CustomerManager.Web.Persistence.Configurations.Entities;

internal sealed class TagConfiguration : AuditConfiguration<Tag>
{
    public override void Configure(EntityTypeBuilder<Tag> builder)
    {
        base.Configure(builder);

        builder.Property(model => model.Title)
            .IsRequired();

        builder.HasIndex(model => model.Title)
            .IsUnique();

        builder.HasMany(model => model.Tasks)
            .WithOne(tagTask => tagTask.Tag)
            .HasForeignKey(tagTask => tagTask.TagId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}