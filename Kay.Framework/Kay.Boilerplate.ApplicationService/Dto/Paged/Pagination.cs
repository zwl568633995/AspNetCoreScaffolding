using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.Dto.Paged
{
    public class Pagination
    {
        //
        // 摘要:
        //     页码
        public int PageIndex { get; set; }
        //
        // 摘要:
        //     每页记录数
        public int PageSize { get; set; }
        //
        // 摘要:
        //     总记录数
        public int Total { get; set; }

    }
}
