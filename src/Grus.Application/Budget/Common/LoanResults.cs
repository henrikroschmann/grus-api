using Grus.Domain.Entities.Budget;

namespace Grus.Application.Budget.Common;

public record LoanResults(IEnumerable<Loan> Budget);