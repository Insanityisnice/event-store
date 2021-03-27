using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Domain.Model;
using EventStore.Persistence;
using Microsoft.Extensions.Logging;

namespace EventStore
{
    internal class EventStream : IEventStream
    {
        private readonly ILogger<EventStream> logger;
        private readonly IStreamStore store;

        private DateTimeOffset after = default(DateTimeOffset);
        private DateTimeOffset before = default(DateTimeOffset);

        public string StreamName { get; private set; }
        public string Category { get; private set; }

        public EventStream(string streamName, IStreamStore store, ILogger<EventStream> logger)
        {
            if (string.IsNullOrWhiteSpace(streamName)) throw new ArgumentException($"'{nameof(streamName)}' cannot be null or whitespace.", nameof(streamName));

            this.StreamName = streamName;
            this.Category = streamName.Split('-').First();

            this.store = store ?? throw new ArgumentNullException(nameof(store));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IAsyncEnumerator<Event> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return store.ReadStream(StreamName).GetAsyncEnumerator(cancellationToken);
        }

        public async Task Add(params Event[] events)
        {
            await store.AddEventsToStream(StreamName, events);
            logger.AddedEvents(StreamName, events.Length);
        }

        public IEventStream After(DateTimeOffset after)
        {
            this.after = after;
            return this;
        }

        public IEventStream Before(DateTimeOffset before)
        {
            this.before = before;
            return this;
        }

        public IEventStream Between(DateTimeOffset after, DateTimeOffset before)
        {
            After(after);
            Before(this.before = before);
            return this;
        }

        public async Task<T> ToObject<T>(T seed, Func<Event, T, T> merge)
        {
            using var scope = logger.MethodScope(nameof(EventStream), nameof(ToObject));

            T obj = seed;
            await foreach(Event @event in store.ReadStream(StreamName, after, before))
            {
                obj = merge(@event, obj);
            }

            return obj;
        }

        public Task<T> ToObject<T>(Func<Event, T, T> merge)
        {
            return ToObject<T>(default(T), merge);
        }
    }
}