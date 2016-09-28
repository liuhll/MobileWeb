using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Jueci.MobileWeb.Repositories
{
    public interface ICpRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        
    }

    public interface ICpRepositoryBase<TEntity> : IRepository<TEntity, int>
      where TEntity : class, IEntity<int>
    {

    }
}