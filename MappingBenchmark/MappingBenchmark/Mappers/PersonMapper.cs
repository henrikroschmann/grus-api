using MappingBenchmark.Dto;
using MappingBenchmark.Models;
using Riok.Mapperly.Abstractions;

namespace MappingBenchmark.Mappers;

public static class PersonMapper
{
    public static PersonDTO MapToDTO(Person person)
    {
        var personDTO = new PersonDTO
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Address = new AddressDTO
            {
                Street = person.Address.Street,
                City = person.Address.City,
                State = person.Address.State,
                ZipCode = person.Address.ZipCode
            },
            PhoneNumbers = new List<PhoneNumberDTO>()
        };

        foreach (var phoneNumber in person.PhoneNumbers)
        {
            personDTO.PhoneNumbers.Add(new PhoneNumberDTO
            {
                Type = phoneNumber.Type,
                Number = phoneNumber.Number
            });
        }

        personDTO.DateOfBirth = person.DateOfBirth.ToString("yyyy-MM-dd");

        return personDTO;
    }
}

[Mapper]
public partial class PersonModelMapperly
{
    public partial PersonDTO MapToDTO(Person person);
}