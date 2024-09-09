using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Application.Helper
{
    public class PaginatedList<T>
{
    public int PageIndex { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public List<T> Items { get; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
}

}