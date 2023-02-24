namespace Grus.Application.Budget.Command.Delete;

public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand, ErrorOr<BudgetResult>>
{
    private readonly IBudgetRepository _budgetRepository;

    public DeleteBudgetCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<ErrorOr<BudgetResult>> Handle(DeleteBudgetCommand command, CancellationToken cancellationToken)
    {
        await _budgetRepository.DeleteOne(command.UserId, command.Id);
        return new ErrorOr<BudgetResult>();
    }
}