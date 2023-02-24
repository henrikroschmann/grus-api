namespace Grus.Application.Budget.Command.Update;

public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, ErrorOr<BudgetResult>>
{
    private readonly IBudgetRepository _budgetRepository;

    public UpdateBudgetCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<ErrorOr<BudgetResult>> Handle(UpdateBudgetCommand command, CancellationToken cancellationToken)
    {
        return default;
    }
}