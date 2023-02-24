using Grus.Domain.Entities.Budget;

namespace Grus.Application.Budget.Command.Create;

public class SaveBudgetCommandHandler : IRequestHandler<SaveBudgetCommand, ErrorOr<BudgetResult>>
{
    private readonly IBudgetRepository _budgetRepository;

    public SaveBudgetCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<ErrorOr<BudgetResult>> Handle(SaveBudgetCommand command, CancellationToken cancellationToken)
    {
        var items = new List<BudgetItem>();

        foreach (var budgetDataItem in command.BudgetModel.Items)
        {
            switch (budgetDataItem.Type)
            {
                case "Bill":
                    items.Add(new Bill(budgetDataItem.Name, budgetDataItem.Amount));
                    break;

                case "Income":
                    items.Add(new Income(budgetDataItem.Name, budgetDataItem.Amount));
                    break;

                case "Savings":
                    items.Add(new Savings(budgetDataItem.Name, budgetDataItem.Amount));
                    break;

                case "Subscription":
                    items.Add(new Subscription(budgetDataItem.Name, budgetDataItem.Amount, budgetDataItem.IsActive ?? false));
                    break;

                case "Loan":
                    items.Add(new Loan(budgetDataItem.Name, budgetDataItem.Amount, budgetDataItem.RemainingAmount ?? 0, budgetDataItem.TotalAmount ?? 0));
                    break;
            }
        }

        var budget = new Domain.Entities.Budget.Budget(
            Id: command.BudgetModel.Id ?? Guid.NewGuid(),
            UserId: command.UserId,
            Items: items,
            CreatedAt: DateTime.Now
        );

        var result = await _budgetRepository.Add(budget);

        return new BudgetResult(result);
    }
}