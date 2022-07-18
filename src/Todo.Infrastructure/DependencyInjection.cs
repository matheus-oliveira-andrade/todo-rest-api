using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.FakeDb;
using Todo.Infrastructure.Interfaces;

namespace Todo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddSingleton<ITodoRepository, FakeTodoRepository>();

        return services;
    }
}