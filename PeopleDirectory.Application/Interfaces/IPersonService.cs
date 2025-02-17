using PeopleDirectory.Application.DTOs;

namespace PeopleDirectory.Application.Interfaces
{
    public interface IPersonService
    {
        Task<int> AddPersonAsync(AddPersonDto dto);
        Task<PersonDto?> GetPersonByIdAsync(int id);
        Task<string?> UploadPersonPhotoAsync(int personId, byte[] fileBytes, string fileName);
        Task<bool> AddRelatedPersonAsync(int personId, RelatedPersonDto relatedPersonDto);
        Task<bool> RemoveRelatedPersonAsync(int personId, int relatedPersonId);
        Task<List<PersonRelationsReportDto>> GetRelatedPersonsReportAsync();
        Task<(List<PersonDto> Persons, int TotalCount)> SearchPersonsAsync(string? firstName, string? lastName, string? personalId, int pageNumber, int pageSize, bool detailed = false);
        Task<bool> EditPersonAsync(int id, EditPersonDto dto);
        Task<bool> DeletePersonAsync(int id);

    }
}
