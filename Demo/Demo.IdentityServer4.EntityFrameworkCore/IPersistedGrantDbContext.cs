using Microsoft.EntityFrameworkCore;

namespace Demo.IdentityServer4.EntityFrameworkCore
{
    public interface IPersistedGrantDbContext
    {
        DbSet<PersistedGrantEntity> PersistedGrants { get; set; }
    }
}
