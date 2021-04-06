using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.Domain.Model;

namespace EventStore
{
    public interface IEventStream : ISequence<Event>, IAsyncEnumerable<Event>
    {
        string StreamName { get; }
        string Category { get; }

        Task Publish(params Event[] events);
    }
}