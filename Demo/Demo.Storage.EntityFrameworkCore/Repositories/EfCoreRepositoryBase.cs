﻿using Demo.Data.Repositories;
using Demo.Data.Uow;
using Demo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Storage.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBase<TDbContext, TEntity> : EfCoreRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : DbContext
    {
        public EfCoreRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider, IUnitOfWorkManager unitOfWorkManager) 
            : base(dbContextProvider, unitOfWorkManager)
        {
        }
    }
}