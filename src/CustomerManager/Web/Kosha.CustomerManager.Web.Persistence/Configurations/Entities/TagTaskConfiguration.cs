using Kosha.CustomerManager.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosha.CustomerManager.Web.Persistence.Configurations.Entities;

internal sealed class TagTaskConfiguration : IEntityTypeConfiguration<TagTask>
{
    public void Configure(EntityTypeBuilder<TagTask> builder)
    {
        builder.HasKey(model => new { model.TagId, model.TaskId });

        builder.Property(model => model.RowVersion)
            .IsRowVersion();
    }
}