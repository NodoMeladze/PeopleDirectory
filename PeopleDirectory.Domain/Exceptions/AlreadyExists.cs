namespace PeopleDirectory.Domain.Exceptions
{
    public class AlreadyExists(string message) : Exception(message) { }
}
