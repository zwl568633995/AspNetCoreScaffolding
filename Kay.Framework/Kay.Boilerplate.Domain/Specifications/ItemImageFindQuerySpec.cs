using Kay.Boilerplate.Domain.Entities;
using Kay.Framework.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Kay.Boilerplate.Domain.Specifications
{
    public class ItemImageFindQuerySpec : BaseSpecification<TbItemImageEntity>
    {
        public ItemImageFindQuerySpec(long itemId) : base(new ItemImageSpec(itemId))
        {
        }
    }


    public class ItemImageSpec : Specification<TbItemImageEntity>
    {
        private long ItemId { get; }

        public ItemImageSpec(long itemId)
        {
            ItemId = itemId;
        }

        public override Expression<Func<TbItemImageEntity, bool>> ToExpression()
        {
            return a => a.ItemId == ItemId;
        }
    }
}
