using Kay.Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kay.Boilerplate.Domain.Entities
{
    [Table("TbItemImage")]
    public class TbItemImageEntity : BizEntity
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageSource { get; set; }
    }
}
