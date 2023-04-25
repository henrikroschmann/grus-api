namespace Grus.Infrastructure.GraphQL.MutationModels;

public class CreateBudgetInput
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public List<IncomeInput> Incomes { get; set; }
    public List<ExpenseInput> Expenses { get; set; }
    public List<SavingsInput> Savings { get; set; }
}