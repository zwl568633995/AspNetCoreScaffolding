using Kay.Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kay.Boilerplate.Domain.Entities
{
    /// <summary>
    /// 店铺实体
    /// </summary>
    [Table("TbShop")]
    public class TbShopEntity : BizEntity
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 店铺地址
        /// </summary>
        public string ShopAddress { get; set; }

        /// <summary>
        /// 店铺电话
        /// </summary>
        public string ShopPhone { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
    }
}
