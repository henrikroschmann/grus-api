using Grus.Application.Budget.Query.GetActiveLoans;
using Grus.Application.Budget.Query.GetActiveSubscriptions;

namespace Grus.Controllers;

[Route("api/budgets")]
public class BudgetController : ApiController
{
    private readonly ISender _mediator;

    public BudgetController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetBudgetById(Guid budgetId)
    {
        if (!Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                out var userId)) return Problem("Not Authorized");

        var query = new GetBudgetByIdQuery(userId, budgetId);
        var budget = await _mediator.Send(query);

        return budget.Match(
            budget => Ok(budget),
            errors => Problem(errors));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> SaveBudget(SaveBudgetRequest request)
    {
        if (!Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var userId))
            return Problem("Not Authorized");

        var command = new SaveBudgetCommand(userId, request.Budget);
        var budget = await _mediator.Send(command);

        return Ok(budget);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateBudget(SaveBudgetRequest request)
    {
        if (!Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                out var userId)) return Problem("Not Authorized");

        //var command = new UpdateBudgetCommand(userId, request.Budget);
        //var result = await _mediator.Send(command);

        //return result.Match(
        //    result => Ok(result),
        //    errors => Problem(errors));
        return default;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllBudgets()
    {
        if (!Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                out var userId)) return Problem("Not Authorized");

        var query = new GetBudgetsQuery(userId);
        var budgets = await _mediator.Send(query);

        return budgets.Match(
            budgets => Ok(budgets),
            errors => Problem(errors));
    }

    [HttpGet("subscriptions")]
    [Authorize]
    public async Task<IActionResult> GetActiveSubscription()
    {
        if (!Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                out var userId)) return Problem("Not Authorized");

        var query = new GetActiveSubscriptionsQuery(userId);
        var budgets = await _mediator.Send(query);

        return budgets.Match(
            budgets => Ok(budgets),
            errors => Problem(errors));
    }

    [HttpGet("loans")]
    [Authorize]
    public async Task<IActionResult> GetActiveLoans()
    {
        if (!Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                out var userId)) return Problem("Not Authorized");

        var query = new GetActiveLoansQuery(userId);
        var budgets = await _mediator.Send(query);

        return budgets.Match(
            budgets => Ok(budgets),
            errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteBudgets(Guid id)
    {
        if (!Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                out var userId)) return Problem("Not Authorized");

        var query = new DeleteBudgetCommand(userId, id);
        var budgets = await _mediator.Send(query);

        return budgets.Match(
            budgets => Ok(budgets),
            errors => Problem(errors));
    }
}