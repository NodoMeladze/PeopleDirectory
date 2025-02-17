using Microsoft.AspNetCore.Mvc;
using PeopleDirectory.Application.DTOs;
using PeopleDirectory.Application.Interfaces;

namespace PeopleDirectory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController(IPersonService personService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] AddPersonDto dto)
        {
            var personId = await personService.AddPersonAsync(dto);
            return CreatedAtAction(nameof(GetPerson), new { id = personId }, new { id = personId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var person = await personService.GetPersonByIdAsync(id);
            if (person == null)
                return NotFound(new { message = "Person not found." });

            return Ok(person);
        }

        [HttpPost("{id}/photo")]
        public async Task<IActionResult> UploadPersonPhoto(int id, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Invalid file upload." });

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            var photoPath = await personService.UploadPersonPhotoAsync(id, fileBytes, file.FileName);
            if (photoPath == null)
                return NotFound(new { message = "Person not found." });

            return Ok(new { message = "Photo uploaded successfully.", photoPath });
        }

        [HttpPost("{id}/related-persons")]
        public async Task<IActionResult> AddRelatedPerson(int id, [FromBody] RelatedPersonDto relatedPersonDto)
        {
            var success = await personService.AddRelatedPersonAsync(id, relatedPersonDto);
            if (!success)
                return BadRequest(new { message = "Invalid related person data or already exists." });

            return Ok(new { message = "Related person added successfully." });
        }

        [HttpDelete("{id}/related-persons/{relatedPersonId}")]
        public async Task<IActionResult> RemoveRelatedPerson(int id, int relatedPersonId)
        {
            var success = await personService.RemoveRelatedPersonAsync(id, relatedPersonId);
            if (!success)
                return NotFound(new { message = "Related person not found." });

            return Ok(new { message = "Related person removed successfully." });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPeople(
            [FromQuery] string? firstName,
            [FromQuery] string? lastName,
            [FromQuery] string? personalId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool detailed = false)
        {
            var (persons, totalCount) = await personService.SearchPersonsAsync(firstName, lastName, personalId, pageNumber, pageSize, detailed);

            return Ok(new
            {
                totalCount,
                persons
            });
        }

        [HttpGet("related-persons-report")]
        public async Task<IActionResult> GetRelatedPersonsReport()
        {
            var report = await personService.GetRelatedPersonsReportAsync();
            return Ok(report);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPerson(int id, [FromBody] EditPersonDto dto)
        {
            var success = await personService.EditPersonAsync(id, dto);
            if (!success)
                return NotFound(new { message = "Person not found or duplicate Personal ID." });

            return Ok(new { message = "Person updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var success = await personService.DeletePersonAsync(id);
            if (!success)
                return NotFound(new { message = "Person not found." });

            return Ok(new { message = "Person deleted successfully." });
        }
    }
}
