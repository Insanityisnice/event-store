using System.Threading.Tasks;
using EventStore.Persistence;
using Microsoft.Extensions.Logging;

namespace EventStore.AcceptanceTests.Drivers
{
    public class StreamsDriver
    {
        private readonly IEventStore eventStore;
        private readonly ILogger<StreamStoreDriver> logger;

        public StreamsDriver(IEventStore eventStore, ILogger<StreamStoreDriver> logger)
        {
            this.eventStore = eventStore ?? throw new System.ArgumentNullException(nameof(eventStore));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public IEventStream GetStream(string streamName)
        {
            var stream = eventStore.Streams[streamName];
            logger.LogInformation($"Got stream '{streamName}'.");
            
            return stream;
        }
    }
}