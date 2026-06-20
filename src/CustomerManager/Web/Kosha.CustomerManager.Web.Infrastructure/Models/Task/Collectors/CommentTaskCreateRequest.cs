using Kosha.CustomerManager.Web.Infrastructure.Configurations;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;

internal record CommentTaskCreateRequest(string User, string Comment) :
    TaskCreateRequest(TaskTypeConfiguration.Comment);