using PeopleDirectory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PeopleDirectory.Domain.Entities
{
    public class PhoneNumber : EntityBase
    {
        [Required(ErrorMessage = "PhoneTypeRequired")]
        public PhoneType Type { get; set; }

        [Required(ErrorMessage = "PhoneNumberRequired")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "PhoneNumberLength")]
        [RegularExpression(@"^[0-9+\-() ]+$", ErrorMessage = "InvalidPhoneNumberFormat")]
        public required string Number { get; set; }
        public int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
