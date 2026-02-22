using System.Collections.Generic;
using System.Security.Claims;

namespace Kosha.CustomerManager.Web.Models.Infrastructure.Models;

public sealed record AccessTokenRequest(IEnumerable<Claim> Claims, int? ValidationTerm = null);