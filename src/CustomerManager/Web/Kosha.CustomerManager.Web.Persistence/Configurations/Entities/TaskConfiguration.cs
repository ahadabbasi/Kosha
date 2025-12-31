using Kosha.CustomerManager.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosha.CustomerManager.Web.Persistence.Configurations.Entities;

internal sealed class TaskConfiguration : AuditConfiguration<Task>
{
    public override void Configure(EntityTypeBuilder<Task> builder)
    {
        base.Configure(builder);

        builder.Property(model => model.Title)
            .IsRequired();

        builder.Property(model => model.Description)
            .IsRequired();

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
    }
}