using System;
using System.Collections.Generic;

namespace Demo.Validation.DataAnnotations
{
    public abstract class CompositeDataAnnotationsAttribute : Attribute
    {
        public abstract IEnumerable<Attribute> GetAttributes();
    }
}