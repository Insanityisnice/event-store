using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStore.Domain.Model;
using EventStore.Persistence;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

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
            var exists = stream != null;

            logger.LogInformation($"Checked stream '{streamName}' for existence. {exists.ToString()}");

            return exists;
        }

        public async Task StreamHasNoEvents(string streamName)
        {
            var readEvents = store.ReadEvents(streamName);
            var events = await readEvents.ToListAsync();

            events.Should().BeEmpty();
        }

        public async Task StreamHasExpectedEvents(string streamName, IEnumerable<Event> expectedEvents)
        {
            var readEvents = store.ReadEvents(streamName);
            var events = await readEvents.ToListAsync();

            events.Should().BeEquivalentTo(expectedEvents, options => options.Excluding(x => x.Timestamp).Excluding(x => x.SequenceNumber));
        }
    }
}