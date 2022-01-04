using System;

namespace SVideo.Domain.Filters
{
    public class PageFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        protected internal static readonly int DefaultPageLimit = 10;

        public PageFilter()
        {
            PageNumber = 1;
            PageSize = DefaultPageLimit;
        }

        public static PageFilter Of(Nullable<int> pageNumber, Nullable<int> pageSize)
        {
            return Of(pageNumber ?? 1, pageSize ?? DefaultPageLimit);
        }

        public static PageFilter Of(int number, int limit)
        {
            PageFilter pr = new PageFilter
            {
                PageNumber = number < 1 ? 1 : number,
                PageSize = limit < 1 ? 1 : limit
            };

            return pr;
        }
    }
}
