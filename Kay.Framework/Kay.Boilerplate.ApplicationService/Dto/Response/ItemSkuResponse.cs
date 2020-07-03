using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.Dto.Response
{
    public class ItemSkuResponse
    {
        /// <summary>
        /// sku标题
        /// </summary>
        public string title { get; set; }

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
        /// 销售量
        /// </summary>
        public int SaleCount { get; set; }

        /// <summary>
        /// 库存量
        /// </summary>
        public int StockCount { get; set; }

        /// <summary>
        /// 备注提醒信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否是商品默认的
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
