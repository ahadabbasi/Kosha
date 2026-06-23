using System;
using Kosha.CustomerManager.Web.Domain.Helper;

namespace Kosha.CustomerManager.Web.Domain.Entities.Tasks;

public class Contact : IEntity, IInserted
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Organization { get; set; }
    
    public string? Post { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public string? Description { get; set; }

    public DateTime Inserted { get; set; }

    public byte[] RowVersion { get; set; }
}