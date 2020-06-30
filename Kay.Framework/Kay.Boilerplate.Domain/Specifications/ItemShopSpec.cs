using Kay.Boilerplate.Domain.Entities;
using Kay.Framework.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Kay.Boilerplate.Domain.Specifications
{
    public class ItemShopFindQuery : BaseSpecification<TbItemShopRelatedEntity>
    {
        public ItemShopFindQuery(long itemId) : base(new ItemShopRelatedSpec(itemId))
        {
        }
    }


    public class ItemShopRelatedSpec : Specification<TbItemShopRelatedEntity>
    {
        private long ItemId { get; }

        public ItemShopRelatedSpec(long itemId)
        {
            ItemId = itemId;
        }

        public override Expression<Func<TbItemShopRelatedEntity, bool>> ToExpression()
        {
            return a => a.ItemId == ItemId;
        }
    }
}
