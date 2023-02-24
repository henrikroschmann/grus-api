namespace Grus.Application.Budget.Query.GetAll;

public record GetBudgetsQuery(Guid UserId) : IRequest<ErrorOr<BudgetResults>>;