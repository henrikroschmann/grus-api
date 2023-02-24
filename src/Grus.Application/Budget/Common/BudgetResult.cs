namespace Grus.Application.Budget.Common;

public record BudgetResult(Domain.Entities.Budget.Budget Budget);
public record BudgetResults(IEnumerable<Domain.Entities.Budget.Budget> Budget);