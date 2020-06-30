using Kay.Boilerplate.ApplicationService.Dto;
using Kay.Boilerplate.ApplicationService.Dto.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.IAppService
{
    public interface ICityAppService: Kay.Framework.RegisterInterfaces.IAppService
    {
        List<CityListResponse> GetAllList();

        string GetLocationCity(double longitude, double latitude);
    }
}
