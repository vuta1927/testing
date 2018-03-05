using Demo.Data.Uow;
using Demo.Domain.Entities;
using Demo.Storage.EntityFrameworkCore;
using Demo.Storage.EntityFrameworkCore.Repositories;

namespace Web.Model
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public class DemoRepositoryBase <TEntity, TPrimaryKey> : EfCoreRepositoryBase<DemoContext, TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
    public DemoRepositoryBase(IDbContextProvider<DemoContext> dbContextProvider, IUnitOfWorkManager unitOfWorkManager)
        : base(dbContextProvider, unitOfWorkManager)
    {
    }
    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="DemoRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class DemoRepositoryBase<TEntity> : DemoRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected DemoRepositoryBase(IDbContextProvider<DemoContext> dbContextProvider, IUnitOfWorkManager unitOfWorkManager)
            : base(dbContextProvider, unitOfWorkManager)
        {
        }
    }
}
