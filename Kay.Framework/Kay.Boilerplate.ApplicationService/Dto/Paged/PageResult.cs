using Kay.Boilerplate.ApplicationService.Dto.Paged;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.Dto
{
    public class PageResult<T>: Pagination
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Records { get; set; }
    }
}
