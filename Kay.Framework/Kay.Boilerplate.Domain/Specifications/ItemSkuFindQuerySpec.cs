using Kay.Boilerplate.Domain.Entities;
using Kay.Framework.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Kay.Boilerplate.Domain.Specifications
{
    public class ItemSkuFindQuerySpec : BaseSpecification<TbItemSkuEntity>
    {
        public ItemSkuFindQuerySpec(long itemId,bool? isDefault) : base(new ItemSkuSpec(itemId, isDefault))
        {
        }
    }


    public class ItemSkuSpec : Specification<TbItemSkuEntity>
    {
        private long ItemId { get; }

        private bool? IsDefault { get; }

        public ItemSkuSpec(long itemId, bool? isDefault)
        {
            ItemId = itemId;
            IsDefault = isDefault;
        }

        public override Expression<Func<TbItemSkuEntity, bool>> ToExpression()
        {
            if (IsDefault==null)
            {
                return a => a.ItemId == ItemId;
            }

            return a => a.ItemId == ItemId && a.IsDefault==IsDefault;
        }
    }
}
