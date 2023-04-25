namespace Grus.Infrastructure.GraphQL.MutationModels;

public class ExpenseInput
{
    public string Category { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public bool Recurring { get; set; }
}