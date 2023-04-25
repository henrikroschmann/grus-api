using BenchmarkDotNet.Attributes;
using Bogus;
using Bogus.Extensions;
using MappingBenchmark.Dto;
using MappingBenchmark.Mappers;
using MappingBenchmark.Models;
using OrderMapper = MappingBenchmark.Mappers.OrderMapper;
using Person = MappingBenchmark.Models.Person;

namespace MappingBenchmark.Benchmarks;

[MemoryDiagnoser]
public class Benchmarks
{
    private SmallModelMapperly _smallModelMapperly = new();
    private LargeModelMapperly _largeModelMapperly = new();
    private ComplexModelMapperly _complexModelMapperly = new();
    private PersonModelMapperly _personModelMapperly = new();

    private List<SmallModel> _smallModel;
    private LargeModel _largeModel;
    private ComplexModel _complexModel;
    private Person _personModel;

    private Order _orderModel;
    private Customer _customerModel;
    private OrderItem _orderItemModel;
    private Product _productModel;

    [GlobalSetup]
    public void Setup()
    {
        _smallModel = new Faker<SmallModel>()
            .RuleFor(m => m.Id, f => f.Random.Number(1, 100))
            .RuleFor(m => m.Name, f => f.Name.FirstName())
            .Generate(10);

        _largeModel = new Faker<LargeModel>()
            .RuleFor(m => m.Id, f => f.Random.Number(1, 100))
            .RuleFor(m => m.Name, f => f.Name.FirstName())
            .RuleFor(m => m.Age, f => f.Random.Number(18, 65))
            .RuleFor(m => m.Email, f => f.Internet.Email())
            .RuleFor(m => m.Address, f => f.Address.FullAddress())
            .Generate();

        _complexModel = new Faker<ComplexModel>()
            .RuleFor(m => m.Id, f => f.Random.Number(1, 100))
            .RuleFor(m => m.Name, f => f.Name.FirstName())
            .RuleFor(m => m.SmallModels, f => _smallModel)
            .RuleFor(m => m.LargeModel, f => _largeModel)
            .Generate();

        _personModel = new Faker<Person>()
            .RuleFor(m => m.Id, f => f.Random.Number(1, 100))
            .RuleFor(m => m.FirstName, f => f.Name.FirstName())
            .RuleFor(m => m.LastName, f => f.Name.LastName())
            .RuleFor(m => m.Address, f => new Address
            {
                Street = f.Address.StreetAddress(),
                City = f.Address.City(),
                State = f.Address.State(),
                ZipCode = f.Address.ZipCode()
            })
            .RuleFor(m => m.PhoneNumbers,
                f => new Faker<PhoneNumber>().RuleFor(p => p.Number, x => x.Phone.PhoneNumber())
                    .RuleFor(p => p.Type, x => x.PickRandom("Home", "Work", "Mobile")).Generate(10))
            .RuleFor(m => m.DateOfBirth, f => f.Date.Past(30).Date)
            .Generate();

        _orderModel = new Faker<Order>()
            .RuleFor(o => o.Id, f => f.Random.Number(1, 100))
            .RuleFor(o => o.Customer, f => customerFaker.Generate())
            .RuleFor(o => o.Items, f => orderItemFaker.Generate(f.Random.Number(1, 5)))
            .RuleFor(o => o.OrderDate, f => f.Date.Past(2))
            .RuleFor(o => o.TotalPrice, (f, o) => o.Items.Sum(i => i.Price));

        _customerModel = new Faker<Customer>()
            .RuleFor(c => c.Id, f => f.Random.Number(1, 100))
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Address, f => new Address
            {
                Street = f.Address.StreetAddress(),
                City = f.Address.City(),
                State = f.Address.State(),
                ZipCode = f.Address.ZipCode()
            })
            .RuleFor(c => c.Orders, f => orderFaker.Generate(f.Random.Number(1, 5)));

        _orderItemModel = new Faker<OrderItem>()
            .RuleFor(i => i.Id, f => f.Random.Number(1, 100))
            .RuleFor(i => i.Product, f => productFaker.Generate())
            .RuleFor(i => i.Quantity, f => f.Random.Number(1, 10))
            .RuleFor(i => i.Price, (f, i) => i.Product.Price * i.Quantity);

        _productModel = new Faker<Product>()
            .RuleFor(p => p.Id, f => f.Random.Number(1, 100))
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => f.Random.Decimal(1, 100));


    }

    [Benchmark]
    public List<SmallModelDTO> StaticMapSmallModelToDto()
    {
        return _smallModel.Select(x => SmallModelMapper.MapToDTO(x)).ToList();
    }

    [Benchmark]
    public LargeModelDTO StaticMapLargeModelToDto()
    {
        return LargeModelMapper.MapToDTO(_largeModel);
    }

    [Benchmark]
    public ComplexModelDTO StaticMapComplexModelToDto()
    {
        return ComplexModelMapper.MapToDTO(_complexModel);
    }

    [Benchmark]
    public PersonDTO StaticMaPersonDto()
    {
        return PersonMapper.MapToDTO(_personModel);
    }

    [Benchmark]
    public OrderDTO StaticMapOrderToDTO()
    {
        return OrderMapper.MapToDTO(_orderFaker);
        //return _orderMapper.MapToDTO(_orderFaker.Generate());
    }

    [Benchmark]
    public List<SmallModelDTO> MapperlyMapSmallModelToDto()
    {
        return _smallModel.Select(x => _smallModelMapperly.MapToDTO(x)).ToList();
    }

    [Benchmark]
    public LargeModelDTO MapperlyMapLargeModelToDto()
    {
        return _largeModelMapperly.MapToDTO(_largeModel);
    }

    [Benchmark]
    public ComplexModelDTO MapperlyMapComplexModelToDto()
    {
        return _complexModelMapperly.MapToDTO(_complexModel);
    }

    [Benchmark]
    public PersonDTO MapperlyMapPersonModelToDto()
    {
        return _personModelMapperly.MapToDTO(_personModel);
    }
}