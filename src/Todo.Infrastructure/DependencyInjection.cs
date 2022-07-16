using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.DynamoDb;
using Todo.Infrastructure.Interfaces;

namespace Todo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITodoRepository, TodoRepository>();

        return services;
    }
}