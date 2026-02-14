using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

public interface ITaskPaginateResponse
{
    Guid Id { get; }

    string Title { get; }

    string Description { get; }

    DateTime Inserted { get; }

}