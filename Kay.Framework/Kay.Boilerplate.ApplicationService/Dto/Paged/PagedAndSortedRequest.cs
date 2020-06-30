using Kay.Boilerplate.ApplicationService.Dto.Paged;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.Dto
{
    public class PagedAndSortedRequest: Pagination
    {   
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sorting { get; set; }

        /// <summary>
        /// 是否正序排列
        /// </summary>
        public bool IsAsc { get; set; }
    }
}
