using Kosha.CustomerManager.Web.Domain.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosha.CustomerManager.Web.Persistence.Configurations;

internal abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(model => model.Id)
            .ValueGeneratedOnAdd();

        builder.HasKey(model => model.Id);

        builder.Property(model => model.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}