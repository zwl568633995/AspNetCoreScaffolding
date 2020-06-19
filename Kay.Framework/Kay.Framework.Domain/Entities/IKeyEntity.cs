using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.Domain.Entities
{
    public interface IKeyEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
