namespace Grus.Application.Budget.Query.GetActiveSubscriptions;

public record GetActiveSubscriptionsQuery(Guid UserId) : IRequest<ErrorOr<SubscriptionResults>>;