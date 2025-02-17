using System.ComponentModel.DataAnnotations;

namespace PeopleDirectory.Domain.Validations
{
    public class SingleAlphabetAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string name)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            bool isGeorgian = name.All(c => IsGeorgianLetter(c));
            bool isLatin = name.All(c => IsLatinLetter(c));

            if (isGeorgian || isLatin)
                return ValidationResult.Success;

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        private static bool IsGeorgianLetter(char c) =>
            c >= '\u10A0' && c <= '\u10FF';

        private static bool IsLatinLetter(char c) =>
            (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
    }
}
