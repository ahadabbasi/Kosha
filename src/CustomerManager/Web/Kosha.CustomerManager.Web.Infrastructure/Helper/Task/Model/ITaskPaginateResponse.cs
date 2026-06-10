using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

public interface ITaskPaginateResponse
{
    /// <summary>
    /// 
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// 
    /// </summary>
    string Title { get; }

    /// <summary>
    /// 
    /// </summary>
    string Description { get; }

    /// <summary>
    /// 
    /// </summary>
    string Inserted { get; }
}