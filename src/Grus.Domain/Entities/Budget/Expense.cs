using HotChocolate.Types;

namespace Grus.Domain.Entities.Budget;
public class Expense
{
    public string Id { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public bool Recurring { get; set; }
}

public class ExpenseType : ObjectType<Expense>
{
    protected override void Configure(IObjectTypeDescriptor<Expense> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.Category).Type<StringType>();
        descriptor.Field(t => t.Description).Type<StringType>();
        descriptor.Field(t => t.Amount).Type<FloatType>();
        descriptor.Field(t => t.Recurring).Type<BooleanType>();
    }
}