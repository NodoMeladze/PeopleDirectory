using PeopleDirectory.Application.Resources;
using PeopleDirectory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PeopleDirectory.Application.DTOs
{
    public class PhoneNumberDto
    {
        [Required(ErrorMessageResourceName = "PhoneTypeRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        public PhoneType Type { get; set; }

        [Required(ErrorMessageResourceName = "PhoneNumberRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        [StringLength(50, MinimumLength = 4, ErrorMessageResourceName = "PhoneNumberLength", ErrorMessageResourceType = typeof(ValidationMessages))]
        [RegularExpression(@"^[0-9+\-() ]+$", ErrorMessageResourceName = "InvalidPhoneNumberFormat", ErrorMessageResourceType = typeof(ValidationMessages))]
        public required string Number { get; set; }
    }
}