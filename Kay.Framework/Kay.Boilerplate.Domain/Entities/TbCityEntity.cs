using Kay.Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kay.Boilerplate.Domain.Entities
{
    [Table("TbCity")]
    public class TbCityEntity : BizEntity
    {
        /// <summary>
        /// 城市首字母
        /// </summary>
        public string CityCase { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 是否热门城市
        /// </summary>
        public bool IsHotCity { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

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
