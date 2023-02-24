namespace Grus.Application.Budget.Query.GetActiveSubscriptions;

public class GetActiveSubscriptionsQueryHandler : IRequestHandler<GetActiveSubscriptionsQuery, ErrorOr<SubscriptionResults>>
{
    private readonly IBudgetRepository _budgetRepository;

    public GetActiveSubscriptionsQueryHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<ErrorOr<SubscriptionResults>> Handle(GetActiveSubscriptionsQuery command, CancellationToken cancellationToken)
    {
        var getAllBudgets = await _budgetRepository.GetActiveSubscriptionsForUser(command.UserId);
        return new SubscriptionResults(getAllBudgets);
    }
}