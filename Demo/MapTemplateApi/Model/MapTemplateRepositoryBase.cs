using Demo.Data.Uow;
using Demo.Domain.Entities;
using Demo.Storage.EntityFrameworkCore;
using Demo.Storage.EntityFrameworkCore.Repositories;

namespace DAL.Model
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public class MapTemplateRepositoryBase <TEntity, TPrimaryKey> : EfCoreRepositoryBase<MapTemplateContext, TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
    public MapTemplateRepositoryBase(IDbContextProvider<MapTemplateContext> dbContextProvider, IUnitOfWorkManager unitOfWorkManager)
        : base(dbContextProvider, unitOfWorkManager)
    {
    }
    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="MapTemplateRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class MapTemplateRepositoryBase<TEntity> : MapTemplateRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected MapTemplateRepositoryBase(IDbContextProvider<MapTemplateContext> dbContextProvider, IUnitOfWorkManager unitOfWorkManager)
            : base(dbContextProvider, unitOfWorkManager)
        {
        }
    }
}
