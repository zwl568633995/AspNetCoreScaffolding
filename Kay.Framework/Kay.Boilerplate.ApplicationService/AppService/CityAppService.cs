using Kay.Boilerplate.ApplicationService.Dto.Response;
using Kay.Boilerplate.ApplicationService.IAppService;
using Kay.Boilerplate.Domain.Entities;
using Kay.Boilerplate.Infrastructure.Treasure;
using Kay.Framework.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.AppService
{
    public class CityAppService : Kay.Framework.Application.Application.Services.AppService, ICityAppService
    {
        private readonly IRepository<TbCityEntity, long> _cityRepository;

        public CityAppService(
           IServiceProvider serviceProvider,
           IRepository<TbCityEntity, long> cityRepository)
           : base(serviceProvider)
        {
            _cityRepository = cityRepository;
        }

        public List<CityListResponse> GetAllList()
        {
            Mapper.Bind<List<TbCityEntity>, List<CityListResponse>>();
            var allData = _cityRepository.ListAll()?.ToList();
            return Mapper.Map<List<CityListResponse>>(allData);
        }

        public string GetLocationCity(double longitude, double latitude)
        {
            Dictionary<string, double> lst = new Dictionary<string, double>();
            List<TbCityEntity> dataList = _cityRepository.ListAll()?.ToList();
            foreach (var item in dataList)
            {
               double dis = DistanceHelper.Distance(latitude, longitude, item.Latitude, item.Longitude);
               lst.Add(item.CityName, dis);
            }

            var result2 = from pair in lst orderby pair.Value select pair.Key;

            return result2.FirstOrDefault();
        }
    }
}
