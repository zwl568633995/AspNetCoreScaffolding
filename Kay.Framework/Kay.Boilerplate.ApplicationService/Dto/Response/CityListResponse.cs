using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.Dto.Response
{
    public class CityListResponse
    {
        /// <summary>
        /// 城市ID
        /// </summary>
        public long Id { get; set; }

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
    }
}
