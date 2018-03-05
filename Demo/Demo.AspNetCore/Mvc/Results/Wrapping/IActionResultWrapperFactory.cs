using Demo.Dependency;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.AspNetCore.Mvc.Results.Wrapping
{
    public interface IActionResultWrapperFactory : ITransientDependency
    {
        IActionResultWrapper CreateFor([NotNull] ResultExecutingContext actionResult);
    }
}