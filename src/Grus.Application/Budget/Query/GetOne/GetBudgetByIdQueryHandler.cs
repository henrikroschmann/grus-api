namespace Grus.Application.Budget.Query.GetOne;

public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, ErrorOr<BudgetResult>>
{
    private readonly IBudgetRepository _budgetRepository;

    public GetBudgetByIdQueryHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<ErrorOr<BudgetResult>> Handle(GetBudgetByIdQuery command, CancellationToken cancellationToken)
    {
        var budget = await _budgetRepository.GetById(command.UserId, command.BudgetId);
        return budget == null ? default(ErrorOr<BudgetResult>) : new BudgetResult(budget);
    }
}