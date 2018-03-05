using Demo.Dependency;
using Microsoft.AspNetCore.Mvc;

namespace Demo.AspNetCore.Mvc.Controllers
{
    public class AppController : Controller, ITransientDependency
    {
    }
}