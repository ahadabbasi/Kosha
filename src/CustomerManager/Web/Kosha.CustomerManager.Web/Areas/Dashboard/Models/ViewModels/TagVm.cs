using System;
using System.ComponentModel.DataAnnotations;
using Kosha.CustomerManager.Web.Infrastructure.Models.Tag;
using Kosha.CustomerManager.Web.Models.Configurations;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public sealed class TagVm : NewTagVm
{
    public TagVm()
    {
        
    }

    public TagVm(TagResponse tag)
    {
        Id = tag.Id;
        Title = tag.Title;
    }

    [Required(ErrorMessage = ErrorMessageConfiguration.DoNotChangeValue)]
    public Guid? Id { get; set; }
}