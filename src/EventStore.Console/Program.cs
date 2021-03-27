using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using System.Threading.Tasks;
using EventStore.Persistence;
using EventStore.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventStore.Console
{
    class Program
    {
        static async Task<int> Main(string[] args) => await BuildCommandLine()
            .UseHost(_ => Host.CreateDefaultBuilder(),
                host =>
                {
                    host.ConfigureServices(services =>
                    {
                        services.AddSingleton<IStreamStore>(new InMemoryStreamStore());
                        //services.AddSingleton<IEventStore>(provider => new EventStore(provider, provider.GetService<ILogger<EventStore>>()));
                    });
                })
            .UseDefaults()
            .Build()
            .InvokeAsync(args, new System.CommandLine.IO.SystemConsole());

        private static CommandLineBuilder BuildCommandLine()
        {
            return null;
        }

        // static void Main(string[] args)
        // {
        //     var eventStore = new EventStore();
        //     var eventStreams = eventStore.Streams();

        //     var stream = eventStreams["Entity-1"];
        //     stream.Add(new Event(Guid.NewGuid(), 
        //         "Entity.Created", 
        //         JObject.FromObject(new { Id = 1, Name = "Entity 1" }).ToString(),
        //         1, DateTimeOffset.UtcNow)
        //     );
        // }
    }
}
