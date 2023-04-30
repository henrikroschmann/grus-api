using HotChocolate.Types;

namespace Grus.Domain.Entities.Budget;

public class SavingsType : ObjectType<Saving>
{
    protected override void Configure(IObjectTypeDescriptor<Saving> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.Name).Type<StringType>();
        descriptor.Field(t => t.Amount).Type<FloatType>();
    }
}