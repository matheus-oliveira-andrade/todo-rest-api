using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Services;

namespace Todo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddScoped<ITodoProvider, TodoProvider>();

        return services;
    }
}