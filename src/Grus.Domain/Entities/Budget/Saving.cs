using HotChocolate.Types;

namespace Grus.Domain.Entities.Budget;

public class Saving
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
}

public class SavingsType : ObjectType<Saving>
{
    protected override void Configure(IObjectTypeDescriptor<Saving> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.Name).Type<StringType>();
        descriptor.Field(t => t.Amount).Type<FloatType>();
    }
}