using Demo.AspNetCore.Mvc.Extensions;
using Demo.AspNetCore.Mvc.Results.Wrapping;
using Demo.Dependency;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.AspNetCore.Mvc.Results
{
    public class AppResultFilter : IResultFilter, ITransientDependency
    {
        private readonly IActionResultWrapperFactory _actionResultWrapperFactory;

        public AppResultFilter(IActionResultWrapperFactory actionResultWrapper)
        {
            _actionResultWrapperFactory = actionResultWrapper;
        }

        public virtual void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }
            
            _actionResultWrapperFactory.CreateFor(context).Wrap(context);
        }

        public virtual void OnResultExecuted(ResultExecutedContext context)
        {
            //no action
        }
    }
}