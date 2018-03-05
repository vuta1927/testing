
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    class MapContextFactory : IDesignTimeDbContextFactory<MapContext>
    {
        public MapContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MapContext>();
            //var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            builder.UseSqlServer("server=127.0.0.1;Database=DemoPro;Integrated Security=true;MultipleActiveResultSets=true");
            return new MapContext(builder.Options, null, null);
        }
    }
}
