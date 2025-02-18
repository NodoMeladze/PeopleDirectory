namespace PeopleDirectory.Application.DTOs
{
    public class SearchPersonQueryDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool Detailed { get; set; } = false;
    }
}
