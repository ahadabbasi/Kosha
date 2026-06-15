using Kosha.CustomerManager.Web.Domain.Entities;
using Kosha.CustomerManager.Web.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kosha.CustomerManager.Web.Persistence.Configurations.Entities;

internal sealed class CategoryConfiguration : AuditConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.HasIndex(model => model.Name)
            .IsUnique();

        builder.Property(model => model.IsDefault)
            .HasConversion<EnumToStringConverter<AnsEnum>>();

        builder.HasMany(model => model.Tasks)
            .WithOne(model => model.Category)
            .HasForeignKey(model => model.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}