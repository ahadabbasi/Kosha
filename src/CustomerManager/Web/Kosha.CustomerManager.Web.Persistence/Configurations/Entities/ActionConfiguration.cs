using Kosha.CustomerManager.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosha.CustomerManager.Web.Persistence.Configurations.Entities;

internal sealed class ActionConfiguration : AuditConfiguration<Action>
{
    public override void Configure(EntityTypeBuilder<Action> builder)
    {
        base.Configure(builder);

        builder.Property(model => model.Description)
            .IsRequired();
    }
}