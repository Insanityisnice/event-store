using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.Domain.Model;

namespace EventStore
{
    public interface IEventStream : IAsyncEnumerable<Event>
    {
        string StreamName { get; }
        string Category { get; }

        Task Add(params Event[] events);
        IEventStream After(DateTimeOffset after);
        IEventStream Before(DateTimeOffset before);
        IEventStream Between(DateTimeOffset after, DateTimeOffset before);
        Task<T> ToObject<T>(T seed, Func<Event, T, T> merge);
        Task<T> ToObject<T>(Func<Event, T, T> merge);
    }
}