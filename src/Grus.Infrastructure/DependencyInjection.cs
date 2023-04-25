using Grus.Infrastructure.Persistence.UserProfile;

namespace Grus.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        var mongoDB = configuration.GetSection("MongoDb");
        var mongoConfigs = mongoDB.GetChildren().ToList();

        services.AddSingleton<IMongoClient>(x => new MongoClient(mongoConfigs.FirstOrDefault(x => x.Key == "ConnectionString")?.Value));
        services.AddScoped(x =>
            x.GetService<IMongoClient>().GetDatabase(mongoConfigs.FirstOrDefault(x => x.Key == "Database")?.Value));

        services.AddAuth(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddHttpContextAccessor();
        services.AddScoped<MongoDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }
}