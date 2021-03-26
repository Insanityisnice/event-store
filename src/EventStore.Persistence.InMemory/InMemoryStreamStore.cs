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
        private ConcurrentDictionary<string, List<Event>> streams = new ConcurrentDictionary<string, List<Event>>();
        
        public Task AddEventsToStream(string streamName, IEnumerable<Event> events)
        {
            var storedEvents = streams.GetOrAdd(streamName, _ => new List<Event>());
            storedEvents.AddRange(events);

            return Task.FromResult<bool>(true);
        }

        public IAsyncEnumerable<Event> ReadStream(string streamName, int firstEvent, int lastEvent)
        {
            var storedEvents = streams[streamName];
            return new AsyncEnumerator(storedEvents.Skip(firstEvent).Take(lastEvent - firstEvent));
        }

        public IAsyncEnumerable<Event> ReadStream(string streamName, DateTimeOffset from = default, DateTimeOffset to = default)
        {
            var storedEvents = streams[streamName];
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
