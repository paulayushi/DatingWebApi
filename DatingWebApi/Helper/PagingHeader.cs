using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebApi.Helper
{
    public class PagingHeader
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public PagingHeader(int currentPage, int itemPerPage, int totalPages, int totalCount)
        {
            CurrentPage = currentPage;
            PageSize = itemPerPage;
            TotalPages = totalPages;
            TotalCount = totalCount;
        }
    }
}
