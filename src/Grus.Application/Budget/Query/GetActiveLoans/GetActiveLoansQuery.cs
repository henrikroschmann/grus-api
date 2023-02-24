namespace Grus.Application.Budget.Query.GetActiveLoans;

public record GetActiveLoansQuery(Guid UserId) : IRequest<ErrorOr<LoanResults>>;