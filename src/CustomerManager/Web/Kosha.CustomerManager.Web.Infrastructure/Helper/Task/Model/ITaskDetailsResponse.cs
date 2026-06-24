using System.Collections.Generic;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

public interface ITaskDetailsResponse
{
    /// <summary>
    /// 
    /// </summary>
    string Title { get; }

    /// <summary>
    /// 
    /// </summary>
    IEnumerable<ITaskDetailsInformationResponse>? Information { get; }
}