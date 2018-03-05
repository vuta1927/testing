using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Demo.Helpers.Extensions;

namespace Demo.Reflection
{
    public class AssemblyFinder : IAssemblyFinder
    {
        public List<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetExcutingAssembiles().ToList();
        }
    }
}