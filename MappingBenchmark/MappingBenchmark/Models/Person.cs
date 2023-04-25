namespace MappingBenchmark.Models;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Address Address { get; set; }
    public List<PhoneNumber> PhoneNumbers { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}

public class PhoneNumber
{
    public string Type { get; set; }
    public string Number { get; set; }
}