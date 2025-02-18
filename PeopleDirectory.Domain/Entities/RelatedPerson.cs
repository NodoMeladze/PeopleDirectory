using PeopleDirectory.Domain.Enums;

namespace PeopleDirectory.Domain.Entities
{
    public class RelatedPerson : EntityBase
    {
        public required ConnectionType ConnectionType { get; set; }
        public required int PersonId { get; set; }
        public Person? Person { get; set; }
        public required int RelatedPersonId { get; set; }
        public Person? Related { get; set; }
    }
}