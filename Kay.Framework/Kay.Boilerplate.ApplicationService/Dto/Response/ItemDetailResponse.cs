using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.Dto.Response
{
    /// <summary>
    /// 商品详情
    /// </summary>
    public class ItemDetailResponse: ItemListReponse
    {
        /// <summary>
        /// 商品介绍
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 店铺
        /// </summary>
        public List<ShopResponse> ShopResponses { get; set; }
    }
}
