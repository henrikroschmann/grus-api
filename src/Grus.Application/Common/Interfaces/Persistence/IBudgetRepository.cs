using Grus.Domain.Entities.Budget;

namespace Grus.Application.Common.Interfaces.Persistence;

public interface IBudgetRepository
{
    Task<Domain.Entities.Budget.Budget?> GetById(Guid userGuid, Guid id);

    Task<Domain.Entities.Budget.Budget> Add(Domain.Entities.Budget.Budget budget);

    Task Update(Domain.Entities.Budget.Budget budget);

    Task DeleteOne(Guid userGuid, Guid budgetGuid);

    Task<IEnumerable<Domain.Entities.Budget.Budget>> GetAll(Guid userGuid);

    Task<IEnumerable<Subscription>> GetActiveSubscriptionsForUser(Guid userId);

    Task<IEnumerable<Loan>> GetAllLoansWithRemainingAmount(Guid userId);
}