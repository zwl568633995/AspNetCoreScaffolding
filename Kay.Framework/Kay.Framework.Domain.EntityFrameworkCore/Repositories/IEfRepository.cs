using Kay.Framework.Domain.Entities;
using Kay.Framework.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.Domain.EntityFrameworkCore.Repositories
{
    public interface IEfRepository<TEntity,in Tkey>: IRepository<TEntity, Tkey>
        where TEntity : class, IKeyEntity<Tkey>
    {
        /// <summary>
        /// 
        /// </summary>
        DbContext DbContext { get; }

        /// <summary>
        /// 
        /// </summary>
        DbSet<TEntity> DbSet { get; }
    }
}
