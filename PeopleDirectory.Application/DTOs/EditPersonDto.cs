using PeopleDirectory.Application.Resources;
using PeopleDirectory.Domain.Enums;
using PeopleDirectory.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeopleDirectory.Application.DTOs
{
    public class EditPersonDto
    {
        [Required(ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "FirstNameLength", ErrorMessageResourceType = typeof(ValidationMessages))]
        [SingleAlphabet(ErrorMessageResourceName = "SingleAlphabet", ErrorMessageResourceType = typeof(ValidationMessages))]
        public required string FirstName { get; init; }

        [Required(ErrorMessageResourceName = "LastNameRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "LastNameLength", ErrorMessageResourceType = typeof(ValidationMessages))]
        [SingleAlphabet(ErrorMessageResourceName = "SingleAlphabet", ErrorMessageResourceType = typeof(ValidationMessages))]
        public required string LastName { get; init; }

        [Required(ErrorMessageResourceName = "InvalidGender", ErrorMessageResourceType = typeof(ValidationMessages))]
        public required Gender Gender { get; init; }

        [Required(ErrorMessageResourceName = "PersonalIdRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        [StringLength(11, MinimumLength = 11, ErrorMessageResourceName = "PersonalIdDigits", ErrorMessageResourceType = typeof(ValidationMessages))]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessageResourceName = "PersonalIdDigits", ErrorMessageResourceType = typeof(ValidationMessages))]
        public required string PersonalId { get; init; }

        [Required(ErrorMessageResourceName = "DateOfBirthRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        [MinAge(18, ErrorMessageResourceName = "MinAge18", ErrorMessageResourceType = typeof(ValidationMessages))]
        public required DateTime DateOfBirth { get; init; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = "InvalidCity", ErrorMessageResourceType = typeof(ValidationMessages))]
        public required string City { get; init; }
        public List<PhoneNumberDto>? PhoneNumbers { get; init; }
    }
}
