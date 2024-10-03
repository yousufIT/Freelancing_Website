using System;
using System.Runtime.Serialization;

namespace CodeSphere.Domain.Models
{
    [DataContract] // Mark the class with DataContract
    public class PaginationMetaData
    {
        [DataMember] // Mark properties with DataMember
        public int PageSize { get; set; }

        [DataMember]
        public int CurrentPage { get; set; }

        [DataMember]
        public int TotalPageCount { get; set; }

        [DataMember]
        public int TotalItemCount { get; set; }

        // Parameterless constructor for serialization
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
