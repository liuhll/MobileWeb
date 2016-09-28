using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Jueci.MobileWeb.EntityFramework.Repositories
{
    public abstract class CpRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<CpDbContext, TEntity, TPrimaryKey>
          where TEntity : class, IEntity<TPrimaryKey>
    {
        //abstract class MobileWebRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<MobileWebDbContext, TEntity, TPrimaryKey>
        public CpRepositoryBase(IDbContextProvider<CpDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public abstract class CpRepositoryBase<TEntity> : CpRepositoryBase<TEntity, int>
       where TEntity : class, IEntity<int>
    {
        protected CpRepositoryBase(IDbContextProvider<CpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}