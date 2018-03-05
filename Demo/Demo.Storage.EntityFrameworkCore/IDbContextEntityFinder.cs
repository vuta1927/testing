using System;
using System.Collections.Generic;
using Demo.Domain.Entities;

namespace Demo.Storage.EntityFrameworkCore
{
    public interface IDbContextEntityFinder
    {
        IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType);
    }
}