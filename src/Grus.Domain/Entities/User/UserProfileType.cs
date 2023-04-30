
using Grus.Domain.Entities.Budget;
using HotChocolate.Types;

namespace Grus.Domain.Entities.User;

public class UserProfileType : ObjectType<UserProfile>
{
    protected override void Configure(IObjectTypeDescriptor<UserProfile> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.FirstName).Type<StringType>();
        descriptor.Field(t => t.LastName).Type<StringType>();
        descriptor.Field(t => t.Email).Type<StringType>();
        descriptor.Field(t => t.Budgets).Type<ListType<BudgetType>>();
    }
}
