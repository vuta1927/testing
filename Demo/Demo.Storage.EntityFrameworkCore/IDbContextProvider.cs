using Microsoft.EntityFrameworkCore;

namespace Demo.Storage.EntityFrameworkCore
{
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}