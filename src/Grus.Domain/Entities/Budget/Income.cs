namespace Grus.Domain.Entities.Budget;

public class Income : BudgetItem
{
    public Income(string Name, decimal Amount) : base(Name, Amount, BudgetItemType.Income)
    {
    }

    public void Deconstruct(out string Name, out decimal Amount)
    {
        Name = this.Name;
        Amount = this.Amount;
    }
}