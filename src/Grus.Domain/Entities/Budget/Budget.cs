using HotChocolate.Types;

namespace Grus.Domain.Entities.Budget;

public class Budget
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public List<Income> Incomes { get; set; }
    public List<Expense> Expenses { get; set; }
    public List<Saving> Savings { get; set; }
}

public class BudgetType : ObjectType<Budget>
{
    protected override void Configure(IObjectTypeDescriptor<Budget> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.Name).Type<StringType>();
        descriptor.Field(t => t.Date).Type<DateType>();
        descriptor.Field(t => t.Incomes).Type<ListType<IncomeType>>();
        descriptor.Field(t => t.Expenses).Type<ListType<ExpenseType>>();
        descriptor.Field(t => t.Savings).Type<ListType<SavingsType>>();
    }
}