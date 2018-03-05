using System;
using System.Collections.Generic;

namespace Demo.Configuration
{
    public interface IValidationConfiguration : IConfigurator
    {
        List<Type> IgnoredTypes { get; }
    }
}