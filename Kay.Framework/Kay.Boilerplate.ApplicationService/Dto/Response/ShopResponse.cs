using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.Dto.Response
{
    public class ShopResponse
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
