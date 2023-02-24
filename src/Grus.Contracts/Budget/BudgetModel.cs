namespace Grus.Contracts.Budget;

public class BudgetData
{
    public Guid? Id { get; set; }
    public Guid? UserId { get; set; }
    public BudgetItem[] Items { get; set; }
}

public class BudgetItem
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
    public bool? IsActive { get; set; }
    public decimal? RemainingAmount { get; set; }
    public decimal? TotalAmount { get; set; }
}