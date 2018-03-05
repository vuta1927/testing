using System;
using System.Threading.Tasks;
using Demo.Dependency;

namespace Demo.Storage.EntityFrameworkCore
{
    public interface IDbContextSeed : ITransientDependency
    {
        Type ContextType { get; }
        Task SeedAsync();
    }
}