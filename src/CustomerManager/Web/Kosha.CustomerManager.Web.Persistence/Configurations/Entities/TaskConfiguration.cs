using Kosha.CustomerManager.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosha.CustomerManager.Web.Persistence.Configurations.Entities;

internal sealed class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(model => model.Id);

        builder.Property(model => model.Type)
            .IsRequired();

        builder.HasMany(model => model.Tags)
            .WithOne(tagTask => tagTask.Task)
            .HasForeignKey(tagTask => tagTask.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(model => model.Actions)
            .WithOne(model => model.Task)
            .HasForeignKey(model => model.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(model => model.Inserted)
            .IsRequired();

        builder.Property(model => model.Modified)
            .IsRequired(false);

        builder.Property(model => model.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}