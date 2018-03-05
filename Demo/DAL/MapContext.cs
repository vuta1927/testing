using Demo.Data.Uow;
using Demo.IdentityServer4;
using Demo.IdentityServer4.EntityFrameworkCore;
using Demo.Storage.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DAL.Model;

namespace DAL
{
    public class MapContext : DataContextBase<MapContext>, IPersistedGrantDbContext
    {
        public MapContext(DbContextOptions<MapContext> options, ICurrentUnitOfWorkProvider currentUnitOfWorkProvider, IMediator eventBus)
            : base(options, currentUnitOfWorkProvider, eventBus)
        {
        }
        public DbSet<PersistedGrantEntity> PersistedGrants { get; set; }
        public DbSet<Road> Roads { get; set; }
    }
}
