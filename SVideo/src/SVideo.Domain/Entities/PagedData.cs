using SVideo.Domain.Filters;
using System;
using System.Collections.Generic;

namespace SVideo.Domain.Entities
{
    public class PagedData<T> where T : class
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public IList<T> Data { get; set; }

        public PagedData() { }

        public PagedData(List<T> content, int pageNumber, int pageLimit, int totalResult) : this(content, PageFilter.Of(pageNumber, pageLimit), totalResult) { }

        public PagedData(List<T> content, PageFilter pr, long totalResult)
        {
            this.Data = content;

            PageNumber = pr.PageNumber;
            PageSize = pr.PageSize;

            TotalPages = (int)Math.Ceiling((double)totalResult / (double)PageSize);

            TotalRecords = (int)totalResult;
        }

        public static PagedData<T> For(List<T> content, PageFilter pr, long totalResult) => new PagedData<T>(content, pr, totalResult);
    }
}
