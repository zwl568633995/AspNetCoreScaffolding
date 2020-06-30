using Kay.Boilerplate.Domain.Entities;
using Kay.Framework.Domain.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Kay.Boilerplate.Infrastructure.BoundedContext.Ef
{
    public class BoilerplateDbContext : BaseDbContext
    {
        public DbSet<TbUserEntity> TbUserEntities { get; set; }

        public DbSet<TbItemEntity> TbItemEntities { get; set; }

        public DbSet<TbItemImageEntity> TbItemImageEntities { get; set; }

        public DbSet<TbCityEntity> TbCityEntities { get; set; }

        public DbSet<TbShopEntity> TbShopEntities { get; set; }

        public DbSet<TbItemShopRelatedEntity> TbItemShopRelatedEntities { get; set; }

        public BoilerplateDbContext(
            DbContextOptions<BoilerplateDbContext> options,
            IConfiguration configuration
            ) : base(options, configuration)
        {
        }
    }
}
