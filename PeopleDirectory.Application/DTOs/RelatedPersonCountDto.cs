using PeopleDirectory.Domain.Enums;

namespace PeopleDirectory.Application.DTOs
{
    public class RelatedPersonCountDto
    {
        public ConnectionType ConnectionType { get; set; }
        public int Count { get; set; }
    }
}