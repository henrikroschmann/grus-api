namespace Grus.Domain.Entities.Budget;

public class Bill : BudgetItem
{
    public Bill(string Name, decimal Amount) : base(Name, Amount, BudgetItemType.Bill)
    {
    }

    public void Deconstruct(out string Name, out decimal Amount)
    {
        Name = this.Name;
        Amount = this.Amount;
    }
}