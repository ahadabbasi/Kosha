using System;
using Kosha.CustomerManager.Web.Domain.Enums;
using Kosha.CustomerManager.Web.Shared.Models;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Category;

public record CategoryResponse(Guid Id, string Name, EnumCaptionResponse<AnsEnum> IsDefault) : 
    CategoryRequest(Name);