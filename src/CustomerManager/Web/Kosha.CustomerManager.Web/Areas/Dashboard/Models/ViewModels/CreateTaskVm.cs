using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

[JsonPolymorphic(TypeDiscriminatorPropertyName = nameof(CreateTaskVm.Type))]
[JsonDerivedType(typeof(CreateCommentTaskVm), "Comment")]
public record CreateTaskVm([Required]string Type) : TaskCreateRequest(Type);

public sealed record CreateCommentTaskVm(string User, string Comment) : CreateTaskVm("Comment");
