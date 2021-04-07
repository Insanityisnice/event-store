using System.Threading.Tasks;
using EventStore.Persistence;
using Microsoft.Extensions.Logging;

namespace EventStore.AcceptanceTests.Drivers
{
    public class StreamStoreDriver
    {
        private readonly IStreamStore store;
        private readonly ILogger<StreamStoreDriver> logger;

        public StreamStoreDriver(IStreamStore store, ILogger<StreamStoreDriver> logger)
        {
            this.store = store ?? throw new System.ArgumentNullException(nameof(store));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Exists(string streamName)
        {
            var stream = await store.ReadStream(streamName);
            return stream != null;
        }
    }
}