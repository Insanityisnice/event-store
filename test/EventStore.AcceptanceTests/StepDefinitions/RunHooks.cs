using EventStore.Persistence;
using EventStore.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SolidToken.SpecFlow.DependencyInjection;
using TechTalk.SpecFlow;

namespace PwC.GTT.Platform.EventStore.Web.IntegrationTests.Support
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
                .ConfigureEventStore();
            
            return services;
        }
    }
}
