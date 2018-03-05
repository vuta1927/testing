using Demo.Dependency;

namespace Demo.Validation
{
    public interface IValidationService : ISingletonDependency
    {
        /// <summary>
        /// Performs validation on the specified <see cref="instance"/>
        /// </summary>
        /// <param name="instance">The instance that is being validated</param>
        /// <returns>A <see cref="ValidationResult"/> instance.</returns>
        ValidationResult ValidateFor(object instance);
    }
}