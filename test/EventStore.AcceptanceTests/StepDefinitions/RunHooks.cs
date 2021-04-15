using EventStore.AcceptanceTests.Drivers;
using EventStore.Persistence;
using EventStore.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;
using TechTalk.SpecFlow;

namespace EventStore.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class RunHooks
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            services.AddLogging()
                .AddSingleton<IStreamStore>(new InMemoryStreamStore()) //TODO: Figure out a way to swap this out or share the tests for testing different persistence technologies. 
                .AddScoped<StreamStoreDriver>()
                .AddScoped<StreamsDriver>()
                .AddScoped<StreamTestContext>()
                .ConfigureEventStore();
            
            return services;
        }
    }
}
