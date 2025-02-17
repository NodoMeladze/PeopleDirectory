using System.ComponentModel.DataAnnotations;

namespace PeopleDirectory.Domain.Validations
{
    public class MinAgeAttribute : ValidationAttribute
    {
        private readonly int _minAge;

        public MinAgeAttribute(int minAge)
        {
            _minAge = minAge;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime dob)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            var age = DateTime.Today.Year - dob.Year;
            if (dob > DateTime.Today.AddYears(-age))
                age--;

            if (age >= _minAge)
                return ValidationResult.Success;

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
