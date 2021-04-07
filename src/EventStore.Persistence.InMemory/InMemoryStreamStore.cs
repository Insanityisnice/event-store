using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Domain.Model;

namespace EventStore.Persistence.InMemory
{
    public class InMemoryStreamStore : IStreamStore
    {
        private ConcurrentDictionary<string, List<Event>> commits = new ConcurrentDictionary<string, List<Event>>();
        private ConcurrentDictionary<string, Stream> streams = new ConcurrentDictionary<string, Stream>();
        
        public Task AddEventsToStream(string streamName, IEnumerable<Event> events)
        {
            var storedEvents = commits.GetOrAdd(streamName, _ => new List<Event>());
            storedEvents.AddRange(events);

            return Task.FromResult<bool>(true);
        }

        public Task<Stream> ReadStream(string streamName)
        {
            if (string.IsNullOrWhiteSpace(streamName)) throw new ArgumentException($"'{nameof(streamName)}' cannot be null or whitespace.", nameof(streamName));

            if (streams.ContainsKey(streamName)) 
            {
                return Task.FromResult(streams[streamName]);
            }

            return Task.FromResult<Stream>(null);
        }

        public IAsyncEnumerable<Event> ReadEvents(string streamName, DateTimeOffset from = default, DateTimeOffset to = default)
        {
            var storedEvents = commits[streamName];
            return new AsyncEnumerator(storedEvents.Where(e => e.Timestamp >= from && e.Timestamp <= to));
        }

        private class AsyncEnumerator : IAsyncEnumerator<Event>, IAsyncEnumerable<Event>
        {
            private readonly IEnumerator<Event> events;

            public AsyncEnumerator(IEnumerable<Event> events)
            {
                this.events = events?.GetEnumerator() ?? throw new ArgumentNullException(nameof(events));
            }

            public Event Current => throw new NotImplementedException();

            public ValueTask DisposeAsync()
            {
                events.Dispose();
                return ValueTask.CompletedTask;
            }

            public IAsyncEnumerator<Event> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return this;
            }

            public ValueTask<bool> MoveNextAsync()
            {
                return ValueTask.FromResult(events.MoveNext());
            }
        }
    }
}
