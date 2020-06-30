using Kay.Boilerplate.ApplicationService.Dto;
using Kay.Boilerplate.ApplicationService.Dto.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.IAppService
{
    public interface IItemAppService : Kay.Framework.RegisterInterfaces.IAppService
    {
        PageResult<ItemListReponse> GetList(PagedAndSortedRequest pagedAndSortedRequest);

        ItemDetailResponse GetDetailById(long itemId);
    }
}
