using System;
using System.Collections;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task;

public record TaskPaginateRequest(string Type, IEnumerable<Guid> Records) : TaskCollectorRequest(Type), ITaskPaginateRequest
{
    public IEnumerator<Guid> GetEnumerator() 
        => Records.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}