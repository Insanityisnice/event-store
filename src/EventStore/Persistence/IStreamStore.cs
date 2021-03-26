using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.Domain.Model;

namespace EventStore.Persistence
{
    public interface IStreamStore
    {
        Task AddEventsToStream(string streamName, IEnumerable<Event> events);
        IAsyncEnumerable<Event> ReadStream(string streamName, DateTimeOffset from = default(DateTimeOffset), DateTimeOffset to = default(DateTimeOffset));
    }
}