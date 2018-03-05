using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Demo.Storage.EntityFrameworkCore
{
    public interface IDbContextResolver
    {
        TDbContext Resolve<TDbContext>(string connectionString, DbConnection existingConnection)
            where TDbContext : DbContext;
    }
}