using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task;

public record TaskPaginateResponse(Guid Id, string Title, string Description, string Inserted) : ITaskPaginateResponse;