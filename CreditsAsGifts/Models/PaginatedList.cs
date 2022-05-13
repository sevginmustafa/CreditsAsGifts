namespace CreditsAsGifts.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> items, int itemsCount, int pageNumber, int itemsPerPage)
        {
            this.PageNumber = pageNumber;
            this.TotalPages = (int)Math.Ceiling(itemsCount / (double)itemsPerPage);

            this.AddRange(items);
        }

        public int PageNumber { get; private set; }

        public int TotalPages { get; private set; }

        public int PreviousPage => this.PageNumber - 1;

        public bool HasPreviousPage => this.PageNumber > 1;

        public int NextPage => this.PageNumber + 1;

        public bool HasNextPage => this.PageNumber < this.TotalPages;

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageNumber, int itemsPerPage)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            return new PaginatedList<T>(items, count, pageNumber, itemsPerPage);
        }
    }
}
