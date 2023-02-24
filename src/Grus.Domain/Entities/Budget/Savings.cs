namespace Grus.Domain.Entities.Budget;

public class Savings : BudgetItem
{
    public Savings(string Name, decimal Amount) : base(Name, Amount, BudgetItemType.Savings)
    {
    }

    public void Deconstruct(out string Name, out decimal Amount)
    {
        Name = this.Name;
        Amount = this.Amount;
    }
}