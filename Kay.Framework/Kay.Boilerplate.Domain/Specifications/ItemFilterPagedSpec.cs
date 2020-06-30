using Kay.Boilerplate.Domain.Entities;
using Kay.Framework.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.Domain.Specifications
{

    public class ItemFilterPagedSpec : BaseSpecification<TbItemEntity>
    {
        /// <summary>
        /// todo:参数是否参加查询需要判断
        /// </summary>
        /// <param name="fileFormatType"></param>
        /// <param name="fileProject"></param>
        public ItemFilterPagedSpec()
            : base(null, ignoreSoftDeleteFilter: false)
        {

        }

        public ItemFilterPagedSpec(IList<long> fileIds, bool ignoreSoftDelete = false)
            : base(m => fileIds.Contains(m.Id), ignoreSoftDelete)
        {
        }
    }
}
