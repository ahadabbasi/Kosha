using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;

public record CommentTaskCollectorRequest(string User, string Comment) : 
    TaskCreateRequest(TaskTypeConfiguration.Comment), 
    ITaskCollectorRequest;