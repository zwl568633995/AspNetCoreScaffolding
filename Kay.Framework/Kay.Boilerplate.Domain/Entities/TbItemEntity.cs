using Kay.Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kay.Boilerplate.Domain.Entities
{
    [Table("TbItem")]
    public class TbItemEntity : BizEntity
    {
        /// <summary>
        /// 城市id
        /// </summary>
        public long CityId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal OriPrice { get; set; }

        /// <summary>
        /// 现价
        /// </summary>
        public decimal DisPrice { get; set; }

        /// <summary>
        /// 返现
        /// </summary>
        public decimal Cashback { get; set; }

        /// <summary>
        /// 销售状态
        /// </summary>
        public int SaleType { get; set; }

        /// <summary>
        /// 销售量
        /// </summary>
        public int SaleCount { get; set; }

        /// <summary>
        /// 库存量
        /// </summary>
        public int StockCount { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 商品介绍
        /// </summary>
        public string Introduction { get; set; }
    }
}
