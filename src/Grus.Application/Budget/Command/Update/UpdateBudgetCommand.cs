namespace Grus.Application.Budget.Command.Update;

public record UpdateBudgetCommand(Guid UserId, Domain.Entities.Budget.Budget Budget) : IRequest<ErrorOr<BudgetResult>>;