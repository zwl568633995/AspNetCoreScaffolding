﻿using Kay.Boilerplate.ApplicationService.Dto;
using Kay.Boilerplate.ApplicationService.Dto.Response;
using Kay.Boilerplate.ApplicationService.IAppService;
using Kay.Boilerplate.Domain.Entities;
using Kay.Boilerplate.Domain.Specifications;
using Kay.Framework.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.AppService
{
    public class ItemAppService : Kay.Framework.Application.Application.Services.AppService, IItemAppService
    {
        private readonly IRepository<TbItemEntity, long> _itemRepository;
        private readonly IRepository<TbItemImageEntity, long> _itemImageRepository;
        private readonly IRepository<TbItemShopRelatedEntity, long> _itemShopRelatedRepository;
        private readonly IRepository<TbShopEntity, long> _shopRepository;
        private readonly IRepository<TbItemSkuEntity, long> _itemSkuRepository;

        public ItemAppService(
           IServiceProvider serviceProvider,
           IRepository<TbItemEntity, long> itemRepository,
           IRepository<TbItemImageEntity, long> itemImageRepository,
           IRepository<TbItemShopRelatedEntity, long> itemShopRelatedRepository,
           IRepository<TbShopEntity, long> shopRepository,
           IRepository<TbItemSkuEntity, long> itemSkuRepository)
           : base(serviceProvider)
        {
            _itemRepository = itemRepository;
            _itemImageRepository = itemImageRepository;
            _itemShopRelatedRepository = itemShopRelatedRepository;
            _shopRepository = shopRepository;
            _itemSkuRepository = itemSkuRepository;
        }

        public ItemDetailResponse GetDetailById(long itemId)
        {
            ItemDetailResponse response = new ItemDetailResponse();
            TbItemEntity tbItemEntity = _itemRepository.GetById(itemId);

            Mapper.Bind<TbItemEntity, ItemDetailResponse> ();
            response = Mapper.Map<ItemDetailResponse> (tbItemEntity);
            response.Covers= _itemImageRepository.List(new ItemImageFindQuerySpec(tbItemEntity.Id))?.Select(y => y.ImageSource).ToList();
            response.ShopResponses = new List<ShopResponse>();
            var shops = _itemShopRelatedRepository.List(new ItemShopFindQuery(tbItemEntity.Id))?.Select(x => x.ShopId).ToList();
            foreach (var shopId in shops)
            {
                TbShopEntity tbShopEntity = _shopRepository.GetById(shopId);
                Mapper.Bind<TbShopEntity, ShopResponse>();
                var shopResponse = Mapper.Map<ShopResponse>(tbShopEntity);
                response.ShopResponses.Add(shopResponse);
            }

            response.SkuResponse = new List<ItemSkuResponse>();
            var defaultSku = _itemSkuRepository.List(new ItemSkuFindQuerySpec(itemId, null))?.ToList();
            Mapper.Bind<List<TbItemSkuEntity>, List<ItemSkuResponse>>();
            response.SkuResponse = Mapper.Map<List<ItemSkuResponse>>(defaultSku);

            ////赋值
            response.OriPrice = defaultSku?.Where(x=>x.IsDefault)?.FirstOrDefault().OriPrice;
            response.DisPrice = defaultSku?.Where(x => x.IsDefault)?.FirstOrDefault().DisPrice;
            response.Cashback = defaultSku?.Where(x => x.IsDefault)?.FirstOrDefault().Cashback;
            response.SaleCount = defaultSku?.Where(x => x.IsDefault)?.FirstOrDefault().SaleCount;
            response.StockCount = defaultSku?.Where(x => x.IsDefault)?.FirstOrDefault().StockCount;

            return response;
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PageResult<ItemListReponse> GetList(PagedAndSortedRequest request)
        {
            var specification = new ItemFilterPagedSpec();
            var total = _itemRepository.Count(specification);
            var result = new PageResult<ItemListReponse>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Total = (int)total
            };
            if (total > 0)
            {
                specification.BuildPaging(request.PageIndex * request.PageSize, request.PageSize);
                specification.BuildSorting(request.Sorting);
                var list = _itemRepository.List(specification).ToList();
                Mapper.Bind<List<TbItemEntity>, List<ItemListReponse>>();
                result.Records = Mapper.Map<List<ItemListReponse>>(list);
                //获取商品的图片
                result.Records.ForEach(x =>
                {
                    x.Covers = _itemImageRepository.List(new ItemImageFindQuerySpec(x.Id))?.Select(y => y.ImageSource).ToList();
                    var defaultSku = _itemSkuRepository.List(new ItemSkuFindQuerySpec(x.Id, true))?.FirstOrDefault();
                    x.OriPrice = defaultSku.OriPrice;
                    x.DisPrice = defaultSku.DisPrice;
                    x.Cashback = defaultSku.Cashback;
                    x.SaleCount = defaultSku.SaleCount;
                    x.StockCount = defaultSku.StockCount;
                });
            }

            return result;
        }
    }
}
