namespace Grus.Application.Budget.Query.GetActiveLoans;

public class GetActiveLoansQueryHandler : IRequestHandler<GetActiveLoansQuery, ErrorOr<LoanResults>>
{
    private readonly IBudgetRepository _budgetRepository;

    public GetActiveLoansQueryHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<ErrorOr<LoanResults>> Handle(GetActiveLoansQuery command, CancellationToken cancellationToken)
    {
        var getAllBudgets = await _budgetRepository.GetAllLoansWithRemainingAmount(command.UserId);
        return new LoanResults(getAllBudgets);
    }
}