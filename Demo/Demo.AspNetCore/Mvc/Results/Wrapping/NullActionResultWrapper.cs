using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.AspNetCore.Mvc.Results.Wrapping
{
    public class NullActionResultWrapper : IActionResultWrapper
    {
        public void Wrap(ResultExecutingContext actionResult)
        {
        }
    }
}