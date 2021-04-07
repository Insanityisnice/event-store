using EventStore;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureEventStore(this IServiceCollection services)
        {
            services.AddSingleton<IEventStore>(provider => new EventStore.EventStore(provider, provider.GetService<ILogger<EventStore.EventStore>>()));
            return services;
        }

    }
}