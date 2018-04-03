
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL.Model
{
    class MapTemplateContextFactory : IDesignTimeDbContextFactory<MapTemplateContext>
    {
        public MapTemplateContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MapTemplateContext>();
            //var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            builder.UseSqlServer("server=127.0.0.1;Database=Demo;User ID=sa;Password=Echo@1927;Integrated Security=false;MultipleActiveResultSets=true");
            return new MapTemplateContext(builder.Options, null, null);
        }
    }
}
