using System;
using System.Runtime.Serialization;

namespace CodeSphere.Domain.Models
{
    public class PaginationMetaData
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPageCount { get; set; }

        public int TotalItemCount { get; set; }

        public PaginationMetaData() { }

        public PaginationMetaData(int totalItemCount, int pageSize, int currentPage)
        {
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)pageSize);
        }
    }
}
