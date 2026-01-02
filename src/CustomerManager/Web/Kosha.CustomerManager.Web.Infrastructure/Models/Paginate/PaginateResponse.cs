using System.Collections;
using System.Collections.Generic;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;

public sealed record PaginateResponse<TData>(
    IEnumerable<TData> Data,
    int TotalRecords,
    int TotalPages,
    int Page,
    int Size
) : IEnumerable<TData>
{
    public IEnumerator<TData> GetEnumerator() 
        => Data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() 
        => GetEnumerator();
}