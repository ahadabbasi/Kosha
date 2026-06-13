using System;
using System.ComponentModel.DataAnnotations;
using Kosha.CustomerManager.Web.Infrastructure.Models.Category;
using Kosha.CustomerManager.Web.Infrastructure.Models.Tag;
using Kosha.CustomerManager.Web.Models.Configurations;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public sealed class TaxonomyVm : NewTaxonomyVm
{
    public TaxonomyVm()
    {
        
    }

    public TaxonomyVm(TagResponse tag)
    {
        Id = tag.Id;
        Title = tag.Title;
    }

    public TaxonomyVm(CategoryResponse category)
    {
        Id = category.Id;
        Title = category.Name;
    }

    [Required(ErrorMessage = ErrorMessageConfiguration.DoNotChangeValue)]
    public Guid? Id { get; set; }
}