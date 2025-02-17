namespace PeopleDirectory.Domain.Entities
{
    public class City : EntityBase
    {
        public required string Name { get; set; }
        public virtual ICollection<Person> Persons { get; set; } = new List<Person>();
    }
}