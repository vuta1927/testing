﻿using System.Linq;

namespace Demo.Validation
{
    /// <summary>
    /// Represents the result of a model validation.
    /// </summary>
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new ValidationErrorCollection();
        }

        public ValidationErrorCollection Errors { get; }

        public bool IsValid => !Errors.Any();
    }
}