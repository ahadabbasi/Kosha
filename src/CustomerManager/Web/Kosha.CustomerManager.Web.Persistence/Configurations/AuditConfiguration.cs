using Kosha.CustomerManager.Web.Domain.Helper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosha.CustomerManager.Web.Persistence.Configurations;

internal abstract class AuditConfiguration<TEntity> : EntityConfiguration<TEntity>
    where TEntity : class, IAudit
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(model => model.Inserted)
            .IsRequired();

        builder.Property(model => model.Modified)
            .IsRequired(false);
    }
}