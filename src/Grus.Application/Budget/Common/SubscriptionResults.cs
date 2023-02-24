using Grus.Domain.Entities.Budget;

namespace Grus.Application.Budget.Common;

public record SubscriptionResults(IEnumerable<Subscription> Budget);