using Kay.Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kay.Boilerplate.Domain.Entities
{
    /// <summary>
    /// 商品和店铺关联
    /// </summary>
    [Table("TbItemShopRelated")]
    public class TbItemShopRelatedEntity : BizEntity
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        public long ShopId { get; set; }
    }
}
