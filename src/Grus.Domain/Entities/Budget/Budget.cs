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
