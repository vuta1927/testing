using Demo.Data.Uow;
using Demo.IdentityServer4;
using Demo.IdentityServer4.EntityFrameworkCore;
using Demo.Mapping;
using Demo.Storage.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Model
{
    public class DemoContext : DataContextBase<DemoContext>, IPersistedGrantDbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options, ICurrentUnitOfWorkProvider currentUnitOfWorkProvider, IMediator eventBus)
            : base(options, currentUnitOfWorkProvider, eventBus)
        {
        }
        public DbSet<PersistedGrantEntity> PersistedGrants { get; set; }
        public DbSet<GoogleRoad> GoogleRoads { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<MapRole> MapRoles { get; set; }
        public DbSet<MapComponent> MapComponents { get; set; }
    }

}
