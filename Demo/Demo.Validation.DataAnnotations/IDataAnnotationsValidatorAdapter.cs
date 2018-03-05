﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.Validation.DataAnnotations
{
    /// <summary>
    /// Adapts DataAnnotations validator attributes into Validators.
    /// </summary>
    public interface IDataAnnotationsValidatorAdapter
    {
        /// <summary>
        /// Gets a boolean that indicates if the adapter can handle the
        /// provided <paramref name="attribute"/>.
        /// </summary>
        /// <param name="attribute">The <see cref="ValidationAttribute"/> that should be handled.</param>
        /// <returns><see langword="true" /> if the attribute can be handles, otherwise <see langword="false" />.</returns>
        bool CanHandle(ValidationAttribute attribute);
        
        /// <summary>
        /// Validates the given instance.
        /// </summary>
        /// <param name="instance">The instance that should be validated.</param>
        /// <param name="attribute">The <see cref="ValidationAttribute"/> that should be handled.</param>
        /// <param name="descriptor">A <see cref="PropertyDescriptor"/> instance for the property that is being validated.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ValidationError"/> instances.</returns>
        IEnumerable<ValidationError> Validate(object instance, ValidationAttribute attribute, PropertyDescriptor descriptor);
    }
}