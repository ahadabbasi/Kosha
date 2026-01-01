using Kosha.CustomerManager.Web.Shared.Helper.Hasher.Models;

namespace Kosha.CustomerManager.Web.Shared.Models.Hasher.Default;

public record HasherResponse(string Hashed) : IHasherResponse;