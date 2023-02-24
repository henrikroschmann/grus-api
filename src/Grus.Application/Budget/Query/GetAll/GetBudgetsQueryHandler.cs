namespace Grus.Application.Budget.Query.GetAll;

public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, ErrorOr<BudgetResults>>
{
    private readonly IBudgetRepository _budgetRepository;

    public GetBudgetsQueryHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<ErrorOr<BudgetResults>> Handle(GetBudgetsQuery command, CancellationToken cancellationToken)
    {
        var getAllBudgets = await _budgetRepository.GetAll(command.UserId);
        return new BudgetResults(getAllBudgets);
    }
}