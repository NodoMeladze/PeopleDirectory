namespace PeopleDirectory.Application.DTOs
{
    public class PersonRelationsReportDto
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<RelatedPersonCountDto> Relations { get; set; } = new();
    }
}
