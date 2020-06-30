using Kay.Boilerplate.ApplicationService.Dto;
using Kay.Boilerplate.ApplicationService.Dto.Response;
using Kay.Boilerplate.ApplicationService.IAppService;
using Kay.Framework.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Kay.Boilerplate.Application.Service.Http.Controllers
{
    /// <summary>
    /// 商品
    /// </summary>
    [Route("item")]
    [ApiController]
    public class ItemController : BaseController
    {
        private readonly IItemAppService _itemAppService;

        public ItemController(IItemAppService itemAppService)
        {
            _itemAppService = itemAppService;
        }


        /// <summary>
        /// 获取商品分页
        /// </summary>
        /// <param name="pagedAndSortedRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getlist")]
        public PageResult<ItemListReponse> GetList(PagedAndSortedRequest pagedAndSortedRequest)
        {
            return _itemAppService.GetList(pagedAndSortedRequest);
        }

        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="itemId">商品Id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("getdetailbyid")]
        public ItemDetailResponse GetDetailById(long itemId)
        {
            return _itemAppService.GetDetailById(itemId);
        }
    }
}