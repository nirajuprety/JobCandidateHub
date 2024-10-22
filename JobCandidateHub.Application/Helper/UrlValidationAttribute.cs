using System.ComponentModel.DataAnnotations;

namespace JobCandidateHub.Application.Helper
{
    public class UrlValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var urlAttribute = new UrlAttribute();
            if (urlAttribute.IsValid(value))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage ?? "Enter a valid URL");
            }
        }
    }
}
