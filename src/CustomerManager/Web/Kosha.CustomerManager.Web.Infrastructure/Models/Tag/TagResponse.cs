using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Tag;

public sealed record TagResponse(Guid Id, string Title) : TagRequest(Title);