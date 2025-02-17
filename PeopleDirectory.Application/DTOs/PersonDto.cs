namespace PeopleDirectory.Application.DTOs
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PersonalId { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public int CityId { get; set; }
        public string? PhotoPath { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; } = new();
        public List<RelatedPersonDto> RelatedPersons { get; set; } = new();
    }
}
