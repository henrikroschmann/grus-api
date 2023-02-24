namespace Grus.Domain.Entities.Budget;

public class Subscription : BudgetItem
{
    public Subscription(string Name, decimal Amount, bool IsActive) : base(Name, Amount, BudgetItemType.Subscription)
    {
        this.IsActive = IsActive;
    }

    public bool IsActive { get; init; }

    public void Deconstruct(out string Name, out decimal Amount, out bool IsActive)
    {
        Name = this.Name;
        Amount = this.Amount;
        IsActive = this.IsActive;
    }
}