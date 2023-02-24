namespace Grus.Application.Budget.Command.Delete;

public record DeleteBudgetCommand(Guid UserId, Guid Id) : IRequest<ErrorOr<BudgetResult>>;