using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;
using System.Text.Json.Serialization;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task;

[JsonPolymorphic(TypeDiscriminatorPropertyName = nameof(TaskCreateRequest.Type))]
[JsonDerivedType(typeof(CommentTaskCollectorRequest), TaskTypeConfiguration.Comment)]
public record TaskCreateRequest(string Type) : ITaskCollectorRequest;