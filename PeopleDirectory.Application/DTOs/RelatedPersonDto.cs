using PeopleDirectory.Application.Resources;
using PeopleDirectory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PeopleDirectory.Application.DTOs
{
    public class RelatedPersonDto
    {
        [Required(ErrorMessageResourceName = "ConnectionTypeRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        public ConnectionType ConnectionType { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = "RelatedPersonIdInvalid", ErrorMessageResourceType = typeof(ValidationMessages))]
        public int RelatedPersonId { get; set; }
    }
}