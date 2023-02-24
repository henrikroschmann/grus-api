namespace Grus.Application.Budget.Query.GetOne;

public record GetBudgetByIdQuery(Guid UserId, Guid BudgetId) : IRequest<ErrorOr<BudgetResult>>;