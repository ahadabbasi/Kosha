using System;
using System.ComponentModel.DataAnnotations;
using Kosha.CustomerManager.Web.Infrastructure.Models.Customer;
using Kosha.CustomerManager.Web.Models.Configurations;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public class EditContactVm : NewContactVm
{
    public EditContactVm()
    {
        
    }

    public EditContactVm(CustomerResponse customer, CustomerContactResponse contact) : base(customer)
    {
        Id = contact.Id;
        Type = contact.Type;
        Value = contact.Value;
    }

    [Required(ErrorMessage = ErrorMessageConfiguration.DoNotChangeValue)]
    public Guid Id { get; set; }
}