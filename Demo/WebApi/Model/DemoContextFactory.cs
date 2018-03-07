
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WebApi.Core.Configuration;
using WebApi.Core.Web;

namespace WebApi.Model
{
    class DemoContextFactory : IDesignTimeDbContextFactory<DemoContext>
    {
        public DemoContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DemoContext>();
            //var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            builder.UseSqlServer("server=127.0.0.1;Database=Demo;User ID=sa;Password=Echo@1927;Integrated Security=true;MultipleActiveResultSets=true");
            return new DemoContext(builder.Options, null, null);
        }
    }
}
