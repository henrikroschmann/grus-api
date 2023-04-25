using Grus.Domain.Entities.Budget;
using Grus.Domain.Entities.User;
using Grus.Infrastructure.GraphQL.MutationModels;
using HotChocolate;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Grus.Infrastructure.GraphQL;

public class Mutation
{
    public async Task<UserProfile> UpdateUserProfileAsync(
        [Service] MongoDbContext dbContext,
        [Service] IHttpContextAccessor httpContextAccessor,
        UpdateUserProfileInput input)
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage("Invalid user token")
                .SetCode("USER_NOT_FOUND")
                .Build());
        }

        var filter = Builders<UserProfile>.Filter.Eq(x => x.Id, userId);
        var update = Builders<UserProfile>.Update
            .Set(x => x.FirstName, input.FirstName)
            .Set(x => x.LastName, input.LastName)
            .Set(x => x.Email, input.Email);

        var options = new FindOneAndUpdateOptions<UserProfile>
        {
            ReturnDocument = ReturnDocument.After
        };

        var updatedUserProfile = await dbContext.UserProfiles.FindOneAndUpdateAsync(filter, update, options);

        if (updatedUserProfile == null)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage("User not found")
                .SetCode("USER_NOT_FOUND")
                .Build());
        }

        return updatedUserProfile;
    }

    public async Task<bool> DeleteUserAsync(
        [Service] MongoDbContext dbContext,
        [Service] IHttpContextAccessor httpContextAccessor)
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage("Invalid user token")
                .SetCode("USER_NOT_FOUND")
                .Build());
        }

        var filter = Builders<UserProfile>.Filter.Eq(x => x.Id, userId);

        var deleteResult = await dbContext.UserProfiles.DeleteOneAsync(filter);

        var userFilter = Builders<User>.Filter.Eq(x => x.Id, userId);
        var deleteUserResult = await dbContext.User.DeleteOneAsync(userFilter);

        if (deleteResult.DeletedCount == 0)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage("User not found")
                .SetCode("USER_NOT_FOUND")
                .Build());
        }

        if (deleteUserResult.DeletedCount == 0)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage("User not found")
                .SetCode("USER_NOT_FOUND")
                .Build());
        }

        return true;
    }

    public async Task<Budget> CreateBudgetAsync(
        [Service] MongoDbContext dbContext,
        [Service] IHttpContextAccessor httpContextAccessor,
        CreateBudgetInput input)
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage("Invalid user token")
                .SetCode("USER_NOT_FOUND")
                .Build());
        }

        var userProfileFilter = Builders<UserProfile>.Filter.Eq(x => x.Id, userId);
        var userProfile = await dbContext.UserProfiles.Find(userProfileFilter).FirstOrDefaultAsync();

        if (userProfile == null)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage("User not found")
                .SetCode("USER_NOT_FOUND")
                .Build());
        }

        var budget = new Budget
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Date = input.Date,
            Incomes = input.Incomes?.Select(i => new Income
            {
                Id = Guid.NewGuid().ToString(),
                Source = i.Source,
                Amount = i.Amount
            }).ToList(),
            Expenses = input.Expenses?.Select(e => new Expense
            {
                Id = Guid.NewGuid().ToString(),
                Category = e.Category,
                Description = e.Description,
                Amount = e.Amount,
                Recurring = e.Recurring
            }).ToList(),
            Savings = input.Savings?.Select(s => new Saving
            {
                Id = Guid.NewGuid().ToString(),
                Name = s.Name,
                Amount = s.Amount
            }).ToList()
        };

        userProfile.Budgets ??= new List<Budget>();
        userProfile.Budgets.Add(budget);

        var userProfileUpdate = Builders<UserProfile>.Update.Set(x => x.Budgets, userProfile.Budgets);

        await dbContext.UserProfiles.UpdateOneAsync(userProfileFilter, userProfileUpdate);

        return budget;
    }

    public async Task<Budget> UpdateBudgetAsync(
        [Service] MongoDbContext dbContext,
        [Service] IHttpContextAccessor httpContextAccessor,
        string id,
        UpdateBudgetInput updatedBudget)
    {
        if (!Guid.TryParse(id, out Guid budgetId)) return default;

        var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage("Invalid user token")
                .SetCode("USER_NOT_FOUND")
                .Build());
        }

        var userProfileFilter = Builders<UserProfile>.Filter.Eq(x => x.Id, userId);
        var userProfile = await dbContext.UserProfiles.Find(userProfileFilter).FirstOrDefaultAsync();

        if (userProfile == null)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage("User not found")
                .SetCode("USER_NOT_FOUND")
                .Build());
        }

        var budgetIndex = userProfile.Budgets.FindIndex(b => b.Id == budgetId);

        if (budgetIndex < 0)
        {
            throw new GraphQLException(new ErrorBuilder()
                .SetMessage("Budget not found")
                .SetCode("BUDGET_NOT_FOUND")
                .Build());
        }

        var originalBudget = userProfile.Budgets[budgetIndex];
        var updatedBudgetObject = new Budget
        {
            Id = budgetId,
            Name = updatedBudget.Name ?? originalBudget.Name,
            Date = originalBudget.Date,
            Incomes = updatedBudget.Incomes?.Select(i => new Income
            {
                Id = Guid.NewGuid().ToString(),
                Source = i.Source,
                Amount = i.Amount
            }).ToList() ?? originalBudget.Incomes,
            Expenses = updatedBudget.Expenses?.Select(e => new Expense
            {
                Id = Guid.NewGuid().ToString(),
                Category = e.Category,
                Description = e.Description,
                Amount = e.Amount,
                Recurring = e.Recurring
            }).ToList() ?? originalBudget.Expenses,
            Savings = updatedBudget.Savings?.Select(s => new Saving
            {
                Id = Guid.NewGuid().ToString(),
                Name = s.Name,
                Amount = s.Amount
            }).ToList() ?? originalBudget.Savings
        };

        userProfile.Budgets[budgetIndex] = updatedBudgetObject;

        var userProfileUpdate = Builders<UserProfile>.Update.Set(x => x.Budgets, userProfile.Budgets);

        await dbContext.UserProfiles.UpdateOneAsync(userProfileFilter, userProfileUpdate);

        return updatedBudgetObject;
    }
}