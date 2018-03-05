using System;
using Demo.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Demo.Storage.EntityFrameworkCore.Configuration
{
    public interface IEfCoreConfiguration : IConfigurator
    {
        void AddDbContext<TDbContext>(Action<DbContextConfiguration<TDbContext>> action)
            where TDbContext : DbContext;
    }
}