using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Commons
{
    public class PaginationResult
    {
        public virtual IEnumerable<object> PageContent { get; set; } = Enumerable.Empty<object>();
        public int TotalItemCount { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int Totaltem {  get; set; }
    }

    public class PaginationResult<T>: PaginationResult
    {
        public new IEnumerable<T> PageContent { get; set; } = Enumerable.Empty<T>();
    }
}
