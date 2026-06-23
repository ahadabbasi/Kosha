using Kosha.CustomerManager.Web.Infrastructure.Configurations;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;

/// <summary>
/// 
/// </summary>
/// <param name="Name"></param>
/// <param name="Organization"></param>
/// <param name="Post"></param>
/// <param name="PhoneNumber"></param>
/// <param name="Description"></param>
internal record ContactTaskCreateRequest(
    string? Name, string? Organization,
    string? Post, string? PhoneNumber, 
    string? Description
) : TaskCreateRequest(TaskTypeConfiguration.Contact);