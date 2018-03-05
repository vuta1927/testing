using Microsoft.EntityFrameworkCore;

namespace Demo.Storage.EntityFrameworkCore.Repositories
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}