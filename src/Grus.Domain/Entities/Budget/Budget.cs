using MongoDB.Bson.Serialization.Attributes;

namespace Grus.Domain.Entities.Budget;

public class Budget
{
    public Budget(Guid? Id,
        Guid? UserId,
        List<BudgetItem> Items,
        DateTime CreatedAt)
    {
        this.Id = Id;
        this.UserId = UserId;
        this.Items = Items;
        this.CreatedAt = CreatedAt;
    }

    public Guid? Id { get; init; }
    public Guid? UserId { get; init; }

    [BsonSerializer(typeof(BudgetItemsSerializer))]
    public List<BudgetItem> Items { get; init; }

    public DateTime CreatedAt { get; init; }

    public void Deconstruct(out Guid? Id, out Guid? UserId, out List<BudgetItem> Items, out DateTime CreatedAt)
    {
        Id = this.Id;
        UserId = this.UserId;
        Items = this.Items;
        CreatedAt = this.CreatedAt;
    }
}

public abstract class BudgetItem
{
    protected BudgetItem(string Name,
        decimal Amount,
        BudgetItemType Type)
    {
        this.Name = Name;
        this.Amount = Amount;
        this.Type = Type;
    }

    public string Name { get; init; }
    public decimal Amount { get; init; }
    public BudgetItemType Type { get; init; }

    public void Deconstruct(out string Name, out decimal Amount, out BudgetItemType Type)
    {
        Name = this.Name;
        Amount = this.Amount;
        Type = this.Type;
    }
}

public enum BudgetItemType
{
    Income,
    Bill,
    Savings,
    Subscription,
    Loan
}