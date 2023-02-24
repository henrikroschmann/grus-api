using Grus.Contracts.Budget;

namespace Grus.Application.Budget.Command.Create;

public record SaveBudgetCommand(Guid UserId, BudgetData BudgetModel) : IRequest<ErrorOr<BudgetResult>>;