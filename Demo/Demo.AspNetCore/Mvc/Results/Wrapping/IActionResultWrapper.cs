using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.AspNetCore.Mvc.Results.Wrapping
{
    public interface IActionResultWrapper
    {
        void Wrap(ResultExecutingContext actionResult);
    }
}