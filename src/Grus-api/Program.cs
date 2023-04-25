using Grus.Application;
using Grus.Common.Errors;
using Grus.Domain.Entities.Budget;
using Grus.Domain.Entities.User;
using Grus.Infrastructure;
using Grus.Infrastructure.GraphQL;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, GrusProblemDetailsFactory>();

    builder.Services.AddGraphQLServer()
        .AddQueryType<Query>()
        .AddMutationType<Mutation>()
        .AddType<UserProfileType>()
        .AddType<BudgetType>()
        .AddType<IncomeType>()
        .AddType<ExpenseType>()
        .AddType<SavingsType>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");

    // Get allowed origins from configuration
    var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
    app.UseCors(x => x.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader());

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseRouting(); // Add this line
    app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapGraphQL();
    });

    app.Run();
}