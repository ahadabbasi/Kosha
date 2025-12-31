using Kosha.CustomerManager.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosha.CustomerManager.Web.Persistence.Configurations.Entities;

internal sealed class CustomerConfiguration : AuditConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.Property(model => model.Name)
            .IsRequired();

        builder.Property(model => model.Family)
            .IsRequired();

        builder.HasMany(model => model.Tasks)
            .WithOne(model => model.Customer)
            .HasForeignKey(model => model.CustomerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(model => model.Contacts)
            .WithOne(model => model.Customer)
            .HasForeignKey(model => model.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}