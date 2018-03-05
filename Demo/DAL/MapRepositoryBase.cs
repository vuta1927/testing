using Demo.Data.Uow;
using Demo.Domain.Entities;
using Demo.Storage.EntityFrameworkCore;
using Demo.Storage.EntityFrameworkCore.Repositories;

namespace DAL
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public class MapRepositoryBase <TEntity, TPrimaryKey> : EfCoreRepositoryBase<MapContext, TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
    public MapRepositoryBase(IDbContextProvider<MapContext> dbContextProvider, IUnitOfWorkManager unitOfWorkManager)
        : base(dbContextProvider, unitOfWorkManager)
    {
    }
    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="MapRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class MapRepositoryBase<TEntity> : MapRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected MapRepositoryBase(IDbContextProvider<MapContext> dbContextProvider, IUnitOfWorkManager unitOfWorkManager)
            : base(dbContextProvider, unitOfWorkManager)
        {
        }
    }
}
