using System.ComponentModel.DataAnnotations;

namespace Demo.Validation.DataAnnotations.Adapters
{
    public class EmailAddressAttributeAdapter : DataAnnotationsValidatorAdapter
    {
        public override bool CanHandle(ValidationAttribute attribute)
        {
            return attribute is EmailAddressAttribute;
        }
    }
}