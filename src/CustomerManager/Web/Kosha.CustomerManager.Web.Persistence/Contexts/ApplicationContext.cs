using Kosha.CustomerManager.Web.Domain.Authenticate;
using Kosha.CustomerManager.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Persistence.Contexts;

internal sealed class ApplicationContext(
    DbContextOptions<ApplicationContext> options
) : DbContext(options)
{
    #region Authentication

    public DbSet<User> Users { get; set; }
    
    public DbSet<Role> Roles { get; set; }

    public DbSet<RoleUser> RoleUsers { get; set; }

    #endregion

    #region Entities

    public DbSet<Tag> Tags { get; set; }

    public DbSet<TagTask> TagTasks { get; set; }

    public DbSet<Task> Tasks { get; set; }

    public DbSet<Action> Actions { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<CustomerContact> CustomerContacts { get; set; }

    public DbSet<Category> Categories { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}