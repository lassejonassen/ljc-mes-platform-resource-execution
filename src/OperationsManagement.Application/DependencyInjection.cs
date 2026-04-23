using Microsoft.Extensions.DependencyInjection;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatorHandlers(typeof(DependencyInjection).Assembly);

        return services;
    }
}