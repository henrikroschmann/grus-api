using budgetEntity = Grus.Domain.Entities.Budget;

namespace Grus.Infrastructure.Persistence.Budget;

public class BudgetRepository : IBudgetRepository
{
    private readonly IMongoCollection<budgetEntity.Budget> _budgets;

    public BudgetRepository(IMongoDatabase database)
    {
        _budgets = database.GetCollection<budgetEntity.Budget>("budgets");
    }

    public async Task<budgetEntity.Budget?> GetById(Guid userGuid, Guid id)
    {
        var budget = await _budgets.FindAsync(x => x.UserId == userGuid && x.Id == id);
        return budget.FirstOrDefault();
    }

    public async Task<budgetEntity.Budget> Add(budgetEntity.Budget budget)
    {
        await _budgets.InsertOneAsync(budget);
        return budget;
    }

    public async Task Update(budgetEntity.Budget budget)
    {
        await _budgets.ReplaceOneAsync(x => x.Id == budget.Id, budget);
    }

    public async Task DeleteOne(Guid userGuid, Guid budgetGuid)
    {
        await _budgets.DeleteOneAsync(x => x.UserId == userGuid && x.Id == budgetGuid);
    }

    public async Task<IEnumerable<budgetEntity.Budget>> GetAll(Guid userGuid)
    {
        var budgets = await _budgets.FindAsync(x => x.UserId == userGuid);
        return budgets.ToList();
    }

    public async Task<IEnumerable<budgetEntity.Subscription>> GetActiveSubscriptionsForUser(Guid userId)
    {
        var budget = await _budgets
            .Find(x => x.UserId == userId)
            .SortByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();

        if (budget == null)
            return Enumerable.Empty<budgetEntity.Subscription>();

        return budget.Items
            .OfType<budgetEntity.Subscription>()
            .Where(x => x.IsActive);
    }

    public async Task<IEnumerable<budgetEntity.Loan>> GetAllLoansWithRemainingAmount(Guid userId)
    {
        var budget = await _budgets
            .Find(x => x.UserId == userId)
            .SortByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();

        if (budget == null)
            return Enumerable.Empty<budgetEntity.Loan>();

        return budget.Items
            .OfType<budgetEntity.Loan>()
            .Where(x => x.RemainingAmount > 0);
    }
}