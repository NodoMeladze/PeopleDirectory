using Microsoft.AspNetCore.Http;

namespace PeopleDirectory.Application.DTOs
{
    public class PhotoDto
    {
        public int Id { get; init; }
        public required IFormFile Photo { get; init; }
    }
}
