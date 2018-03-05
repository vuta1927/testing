using System;
using Demo.Data.Uow;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Storage.EntityFrameworkCore
{
    public interface IEfGenericRepositoryRegistrar
    {
        void RegisterForDbContext(Type dbContextType, IServiceCollection services, AutoRepositoryTypesAttribute defaultAutoRepositoryTypesAttribute);
    }
}