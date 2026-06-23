using Kosha.CustomerManager.Web.Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosha.CustomerManager.Web.Persistence.Configurations.Entities.Tasks;

internal sealed class ContactConfiguration : EntityConfiguration<Contact>
{
    public override void Configure(EntityTypeBuilder<Contact> builder)
    {
        base.Configure(builder);

        builder.Property(model => model.Name)
            .IsRequired(false);

        builder.Property(model => model.Organization)
            .IsRequired(false);

        builder.Property(model => model.Post)
            .IsRequired(false);

        builder.Property(model => model.PhoneNumber)
            .IsRequired(false);

        builder.Property(model => model.Description)
            .IsRequired(false);
    }
}