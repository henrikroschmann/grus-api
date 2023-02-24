namespace Grus.Domain.Entities.Budget;

public class Loan : BudgetItem
{
    public Loan(string Name, decimal Amount, decimal RemainingAmount, decimal TotalAmount) : base(Name, Amount, BudgetItemType.Loan)
    {
        this.TotalAmount = TotalAmount;
        this.RemainingAmount = RemainingAmount;
    }

    public decimal TotalAmount { get; init; }
    public decimal RemainingAmount { get; init; }

    public void Deconstruct(out string Name, out decimal Amount, out decimal RemainingAmount, out decimal TotalAmount)
    {
        Name = this.Name;
        Amount = this.Amount;
        RemainingAmount = this.RemainingAmount;
        TotalAmount = this.TotalAmount;
    }
}