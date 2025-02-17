using Microsoft.AspNetCore.Hosting;
using PeopleDirectory.Application.DTOs;
using PeopleDirectory.Application.Interfaces;
using PeopleDirectory.Domain.Entities;
using PeopleDirectory.Domain.Interfaces;

namespace PeopleDirectory.Application.Services
{
    public class PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork, IWebHostEnvironment environment) : IPersonService
    {
        public async Task<int> AddPersonAsync(AddPersonDto dto)
        {
            var existingPerson = await personRepository.GetByPersonalIdAsync(dto.PersonalId);
            if (existingPerson != null)
            {
                throw new Exception("A person with this Personal ID already exists.");
            }

            var person = new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                PersonalId = dto.PersonalId,
                DateOfBirth = dto.DateOfBirth,
                CityId = dto.CityId
            };

            if (dto.PhoneNumbers != null)
            {
                person.PhoneNumbers = dto.PhoneNumbers.Select(pn => new PhoneNumber
                {
                    Type = pn.Type,
                    Number = pn.Number
                }).ToList();
            }

            await personRepository.AddAsync(person);
            await unitOfWork.SaveChangesAsync();
            return person.Id;
        }

        public async Task<PersonDto?> GetPersonByIdAsync(int id)
        {
            var person = await personRepository.GetPersonFullDetailsAsync(id);
            if (person == null) return null;

            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                PersonalId = person.PersonalId,
                DateOfBirth = person.DateOfBirth,
                CityId = person.CityId,
                PhotoPath = person.PhotoPath,
                PhoneNumbers = person.PhoneNumbers.Select(pn => new PhoneNumberDto
                {
                    Type = pn.Type,
                    Number = pn.Number
                }).ToList(),
                RelatedPersons = person.RelatedPersons.Select(rp => new RelatedPersonDto
                {
                    ConnectionType = rp.ConnectionType,
                    RelatedPersonId = rp.RelatedPersonId
                }).ToList()
            };
        }

        public async Task<string?> UploadPersonPhotoAsync(int personId, byte[] fileBytes, string fileName)
        {
            var person = await personRepository.GetByIdAsync(personId);
            if (person == null)
            {
                return null;
            }

            var uploadDirectory = Path.Combine(environment.WebRootPath, "Uploads");
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var fileExtension = Path.GetExtension(fileName);
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadDirectory, uniqueFileName);

            await File.WriteAllBytesAsync(filePath, fileBytes);

            person.PhotoPath = $"/Uploads/{uniqueFileName}";
            await unitOfWork.SaveChangesAsync();

            return person.PhotoPath;
        }

        public async Task<bool> AddRelatedPersonAsync(int personId, RelatedPersonDto relatedPersonDto)
        {
            var person = await personRepository.GetByIdAsync(personId);
            var relatedPerson = await personRepository.GetByIdAsync(relatedPersonDto.RelatedPersonId);

            if (person == null || relatedPerson == null || personId == relatedPersonDto.RelatedPersonId)
            {
                return false;
            }

            bool alreadyExists = person.RelatedPersons.Any(rp => rp.RelatedPersonId == relatedPersonDto.RelatedPersonId);
            if (alreadyExists)
            {
                return false;
            }

            person.RelatedPersons.Add(new RelatedPerson
            {
                ConnectionType = relatedPersonDto.ConnectionType,
                PersonId = personId,
                RelatedPersonId = relatedPersonDto.RelatedPersonId
            });

            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRelatedPersonAsync(int personId, int relatedPersonId)
        {
            var person = await personRepository.GetByIdAsync(personId);
            if (person == null)
            {
                return false;
            }

            var relatedPerson = person.RelatedPersons.FirstOrDefault(rp => rp.RelatedPersonId == relatedPersonId);
            if (relatedPerson == null)
            {
                return false;
            }

            person.RelatedPersons.Remove(relatedPerson);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<PersonRelationsReportDto>> GetRelatedPersonsReportAsync()
        {
            var persons = await personRepository.GetAllAsync();

            var reportData = persons
                .Where(p => p.RelatedPersons.Any())
                .Select(p => new PersonRelationsReportDto
                {
                    PersonId = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Relations = p.RelatedPersons
                        .GroupBy(rp => rp.ConnectionType)
                        .Select(group => new RelatedPersonCountDto
                        {
                            ConnectionType = group.Key,
                            Count = group.Count()
                        })
                        .ToList()
                })
                .ToList();

            return reportData;
        }

        public async Task<(List<PersonDto> Persons, int TotalCount)> SearchPersonsAsync(string? firstName, string? lastName, string? personalId, int pageNumber, int pageSize, bool detailed = false)
        {
            var (persons, totalCount) = await personRepository.SearchPersonsAsync(firstName, lastName, personalId, pageNumber, pageSize, detailed);

            var personDtos = persons.Select(p => new PersonDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PersonalId = p.PersonalId,
                DateOfBirth = p.DateOfBirth,
                CityId = p.CityId,
                PhotoPath = p.PhotoPath,
                PhoneNumbers = p.PhoneNumbers.Select(pn => new PhoneNumberDto
                {
                    Type = pn.Type,
                    Number = pn.Number
                }).ToList(),
                RelatedPersons = p.RelatedPersons.Select(rp => new RelatedPersonDto
                {
                    ConnectionType = rp.ConnectionType,
                    RelatedPersonId = rp.RelatedPersonId
                }).ToList()
            }).ToList();

            return (personDtos, totalCount);
        }

        public async Task<bool> EditPersonAsync(int id, EditPersonDto dto)
        {
            var person = await personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return false;
            }

            if (person.PersonalId != dto.PersonalId)
            {
                var existingPerson = await personRepository.GetByPersonalIdAsync(dto.PersonalId);
                if (existingPerson != null && existingPerson.Id != id)
                {
                    throw new Exception("A person with this Personal ID already exists.");
                }
            }

            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            person.Gender = dto.Gender;
            person.PersonalId = dto.PersonalId;
            person.DateOfBirth = dto.DateOfBirth;
            person.CityId = dto.CityId;
            person.ModifiedDate = DateTime.UtcNow;

            if (dto.PhoneNumbers != null)
            {
                person.PhoneNumbers.Clear();
                var phoneNumbers = dto.PhoneNumbers.Select(pn => new PhoneNumber
                {
                    Type = pn.Type,
                    Number = pn.Number,
                    PersonId = person.Id
                }).ToList();
                person.PhoneNumbers = phoneNumbers;
            }

            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            var person = await personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return false;
            }

            person.IsDeleted = true;
            person.ModifiedDate = DateTime.UtcNow;

            await unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
