using Microsoft.AspNetCore.Http;
using PeopleDirectory.Application.DTOs;

namespace PeopleDirectory.Application.Interfaces
{
    public interface IPersonService
    {
        Task<int> AddPersonAsync(AddPersonDto dto);
        Task<PersonDto?> GetPersonByIdAsync(int id);
        Task EditPersonAsync(int id, EditPersonDto dto);
        Task DeletePersonAsync(int id);
        Task<string?> UploadPersonPhotoAsync(int id, IFormFile file);
        Task<bool> AddRelatedPersonAsync(int personId, RelatedPersonDto relatedPersonDto);
        Task<bool> RemoveRelatedPersonAsync(int personId, int relatedPersonId);
        Task<List<PersonRelationsReportDto>> GetRelatedPersonsReportAsync();
        Task<(List<PersonDto> Persons, int TotalCount)> SearchPersonsAsync(SearchPersonQueryDto query);
    }
}
