using Kosha.CustomerManager.Web.Infrastructure.Helper.Hasher.Models;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Hasher.Default;

public record HasherResponse(string Hashed) : IHasherResponse;