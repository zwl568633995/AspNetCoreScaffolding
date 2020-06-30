using Kay.Boilerplate.ApplicationService.Dto.Response;
using Kay.Boilerplate.ApplicationService.IAppService;
using Kay.Framework.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kay.Boilerplate.Application.Service.Http.Controllers
{
    /// <summary>
    /// 城市站点
    /// </summary>
    [Route("city")]
    [ApiController]
    public class CityController : BaseController
    {
        private readonly ICityAppService _cityAppService;

        public CityController(ICityAppService cityAppService)
        {
            _cityAppService = cityAppService;
        }


        /// <summary>
        /// 获取所有城市
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getalllist")]
        public List<CityListResponse> GetAllList()
        {
            return _cityAppService.GetAllList();
        }

        /// <summary>
        /// 获取当前定位城市
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getlocationcity")]
        public string GetLocationCity(double longitude,double latitude)
        {
            return _cityAppService.GetLocationCity(longitude, latitude);
        }
    }
}
