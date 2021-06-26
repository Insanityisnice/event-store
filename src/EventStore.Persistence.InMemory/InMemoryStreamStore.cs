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
        private ConcurrentDictionary<string, StoredStream> streams = new ConcurrentDictionary<string, StoredStream>();
        
        public Task AddEventsToStream(string streamName, IEnumerable<Event> events)
        {
            var stream = streams.GetOrAdd(streamName, sn => new StoredStream(sn, 0));
            
            var storedEvents = commits.GetOrAdd(streamName, _ => new List<Event>());
            storedEvents.AddRange(events.Select((e, index) => new Event(e, index + stream.Revision)));

            stream.IncrementRevision(events.Count());

            return Task.FromResult<bool>(true);
        }

        public Task<Stream> ReadStream(string streamName)
        {
            if (string.IsNullOrWhiteSpace(streamName)) throw new ArgumentException($"'{nameof(streamName)}' cannot be null or whitespace.", nameof(streamName));

            if (streams.ContainsKey(streamName)) 
            {
                return Task.FromResult(streams[streamName] as Stream);
            }

            return Task.FromResult<Stream>(null);
        }

        public IAsyncEnumerable<Event> ReadEvents(string streamName, DateTimeOffset from = default(DateTimeOffset), DateTimeOffset to = default(DateTimeOffset))
        {
            if (from == default(DateTimeOffset)) from = DateTimeOffset.MinValue;
            if (to == default(DateTimeOffset)) to = DateTimeOffset.MaxValue;

            if (streams.ContainsKey(streamName))
            {
                var storedEvents = commits[streamName];
                var requestedEvents = storedEvents.Where(e => e.Timestamp >= from && e.Timestamp <= to).Select(e => new HydratedEvent(e));
                return new AsyncEnumerator(requestedEvents);
            }

            return new AsyncEnumerator(Enumerable.Empty<Event>());
        }

        private class StoredStream : Stream 
        {
            public StoredStream(string streamName, int revision)
                : base(streamName, revision)
            {
            }

            public void IncrementRevision(int count)
            {
                Revision += count;
            }
        }

        private class HydratedEvent : Event
        {
            public HydratedEvent(Event @event)
                : base(@event, @event.SequenceNumber)
            {
            }
        }

        private class AsyncEnumerator : IAsyncEnumerator<Event>, IAsyncEnumerable<Event>
        {
            private readonly IEnumerator<Event> events;

            public AsyncEnumerator(IEnumerable<Event> events)
            {
                this.events = events?.GetEnumerator() ?? throw new ArgumentNullException(nameof(events));
            }

            public Event Current => events.Current;

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
