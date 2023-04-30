namespace Grus.Domain.Entities.Budget;
public class Expense
{
    public string Id { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public bool Recurring { get; set; }
}
