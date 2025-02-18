namespace PeopleDirectory.Domain.Exceptions
{
    public class ValidationException(string message) : Exception(message) {}
}
