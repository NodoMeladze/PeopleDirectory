using PeopleDirectory.Domain.Enums;

namespace PeopleDirectory.Domain.Entities
{
    public class RelatedPerson : EntityBase
    {
        public ConnectionType ConnectionType { get; set; }
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        public int RelatedPersonId { get; set; }
        public Person? Related { get; set; }
    }
}