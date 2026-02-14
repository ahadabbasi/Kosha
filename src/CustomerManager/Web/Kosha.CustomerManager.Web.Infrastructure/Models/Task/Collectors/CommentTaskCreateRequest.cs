using Kosha.CustomerManager.Web.Infrastructure.Configurations;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;

public record CommentTaskCreateRequest(string User, string Comment) :
    TaskCreateRequest(TaskTypeConfiguration.Comment);