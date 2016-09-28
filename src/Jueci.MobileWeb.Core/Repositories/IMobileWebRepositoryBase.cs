using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Jueci.MobileWeb.Repositories
{
    public interface IMobileWebRepositoryBase <TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
    }

    public interface IMobileWebRepositoryBase<TEntity> : IRepository<TEntity, int>
      where TEntity : class, IEntity<int>
    {
    }
}
