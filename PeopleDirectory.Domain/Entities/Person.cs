using PeopleDirectory.Domain.Enums;
using PeopleDirectory.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeopleDirectory.Domain.Entities
{
    public class Person : EntityBase
    {
        [Required(ErrorMessage = "FirstNameRequired")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "FirstNameLength")]
        [SingleAlphabet(ErrorMessage = "SingleAlphabet")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "LastNameRequired")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "LastNameLength")]
        [SingleAlphabet(ErrorMessage = "SingleAlphabet")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "GenderRequired")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "PersonalIdRequired")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "PersonalIdLength")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "PersonalIdDigits")]
        public required string PersonalId { get; set; }

        [Required(ErrorMessage = "DateOfBirthRequired")]
        [MinAge(18, ErrorMessage = "MinAge18")]
        public DateTime DateOfBirth { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "InvalidCity")]
        public int CityId { get; set; }
        public City? City { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public string? PhotoPath { get; set; }
        public ICollection<RelatedPerson> RelatedPersons { get; set; } = new List<RelatedPerson>();
    }
}
