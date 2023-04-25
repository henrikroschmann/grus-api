namespace MappingBenchmark.Dto;

public class PersonDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AddressDTO Address { get; set; }
    public List<PhoneNumberDTO> PhoneNumbers { get; set; }
    public string DateOfBirth { get; set; }
}

public class AddressDTO
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}

public class PhoneNumberDTO
{
    public string Type { get; set; }
    public string Number { get; set; }
}