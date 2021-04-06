using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Domain.Model;
using EventStore.Persistence;
using Microsoft.Extensions.Logging;

namespace EventStore
{
    internal class EventStream : Sequence<Event>, IEventStream
    {
        private readonly ILogger<EventStream> logger;
        private readonly IStreamStore store;

        public string StreamName { get; private set; }
        public string Category { get; private set; }

        public EventStream(string streamName, IStreamStore store, ILogger<EventStream> logger)
        {
            if (string.IsNullOrWhiteSpace(streamName)) throw new ArgumentException($"'{nameof(streamName)}' cannot be null or whitespace.", nameof(streamName));

            this.StreamName = streamName;
            this.Category = streamName.Split('-').First(); //TODO:JS:S: Make the category seperator configurable.

            this.store = store ?? throw new ArgumentNullException(nameof(store));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        override public IAsyncEnumerator<Event> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return store.ReadStream(StreamName, after, before).GetAsyncEnumerator(cancellationToken);
        }

        public async Task Publish(params Event[] events)
        {
            using var scope = logger.MethodScope(nameof(EventStream), nameof(EventStream.Publish));

            if (events is null) { logger.AttemptToAddNullEvents(StreamName); throw new ArgumentNullException(nameof(events)); }
            if (events.Length == 0) { logger.AttemptToAddEmptyEvents(StreamName); throw new ArgumentException("There must be at least one event to add to the stream.", nameof(events)); }

            await store.AddEventsToStream(StreamName, events);
            logger.AddedEvents(StreamName, events.Length);
        }

        override public async Task<T> ToObject<T>(T seed, Func<Event, T, T> merge)
        {
            using var scope = logger.MethodScope(nameof(EventStream), nameof(ToObject));

            T obj = seed;
            await foreach(Event @event in this)
            {
                obj = merge(@event, obj);
            }

            return obj;
        }
    }
}