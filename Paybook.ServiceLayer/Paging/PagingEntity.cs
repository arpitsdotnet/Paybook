using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paybook.ServiceLayer.Paging
{
    public class PagingEntity
    {
        public int TotalPages { get; set; }
        // public int PageSize { get; set; }
        // public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
    }
}
