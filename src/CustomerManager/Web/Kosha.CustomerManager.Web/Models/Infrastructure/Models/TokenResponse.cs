using System;

namespace Kosha.CustomerManager.Web.Models.Infrastructure.Models;

public sealed record TokenResponse(string Token, int ValidationTerm, DateTime ExpirationTime);