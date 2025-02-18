using Microsoft.AspNetCore.Mvc;
using PeopleDirectory.Application.DTOs;
using PeopleDirectory.Application.Interfaces;
using PeopleDirectory.Application.Resources;

namespace PeopleDirectory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController(IPersonService personService) : ControllerBase
    {
        [HttpPost("add-person")]
        public async Task<IActionResult> AddPerson([FromBody] AddPersonDto dto)
        {
            var personId = await personService.AddPersonAsync(dto);
            return CreatedAtAction(nameof(GetPerson), new { id = personId }, null);
        }

        [HttpGet("get-person/{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var person = await personService.GetPersonByIdAsync(id);
            return Ok(person);
        }

        [HttpPut("edit-person/{id}")]
        public async Task<IActionResult> EditPerson(int id, [FromBody] EditPersonDto dto)
        {
            await personService.EditPersonAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("delete-person/{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            await personService.DeletePersonAsync(id);
            return NoContent();
        }

        [HttpPost("upload-person-photo/{id}")]
        public async Task<IActionResult> UploadPersonPhoto(int id, IFormFile file)
        {

            var photoPath = await personService.UploadPersonPhotoAsync(id, file);

            return photoPath == null
                ? NotFound(new { message = ValidationMessages.PersonDoesNotExist })
                : Ok(new { photoPath });
        }

        [HttpPost("add-related-person/{id}")]
        public async Task<IActionResult> AddRelatedPerson(int id, [FromBody] RelatedPersonDto relatedPersonDto)
        {
            var success = await personService.AddRelatedPersonAsync(id, relatedPersonDto);
            return success ? NoContent() : BadRequest();
        }

        [HttpDelete("person/{id}/delete-related-person/{relatedPersonId}")]
        public async Task<IActionResult> RemoveRelatedPerson(int id, int relatedPersonId)
        {
            var success = await personService.RemoveRelatedPersonAsync(id, relatedPersonId);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("search-people")]
        public async Task<IActionResult> SearchPeople([FromQuery] SearchPersonQueryDto query)
        {
            var (persons, totalCount) = await personService.SearchPersonsAsync(query);

            return Ok(new { totalCount, persons });
        }

        [HttpGet("get-related-persons-report")]
        public async Task<IActionResult> GetRelatedPersonsReport()
        {
            var report = await personService.GetRelatedPersonsReportAsync();
            return Ok(report);
        }
    }
}
