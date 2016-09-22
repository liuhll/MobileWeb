using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Jueci.MobileWeb.EntityFramework.Repositories
{
    public abstract class MobileWebRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<MobileWebDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected MobileWebRepositoryBase(IDbContextProvider<MobileWebDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class MobileWebRepositoryBase<TEntity> : MobileWebRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected MobileWebRepositoryBase(IDbContextProvider<MobileWebDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
