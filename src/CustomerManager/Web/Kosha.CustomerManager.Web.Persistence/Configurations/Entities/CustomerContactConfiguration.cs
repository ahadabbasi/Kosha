using Kosha.CustomerManager.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosha.CustomerManager.Web.Persistence.Configurations.Entities;

internal sealed class CustomerContactConfiguration : AuditConfiguration<CustomerContact>
{
    public override void Configure(EntityTypeBuilder<CustomerContact> builder)
    {
        base.Configure(builder);

        builder.Property(model => model.Type)
            .IsRequired();

        builder.Property(model => model.Value)
            .IsRequired();

        builder.HasIndex(model => new { model.Type, model.Value, model.CustomerId });

    }
}