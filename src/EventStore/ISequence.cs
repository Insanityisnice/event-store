using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStore
{
    public interface ISequence<TSequenceItem> : IAsyncEnumerable<TSequenceItem>
    {
        ISequence<TSequenceItem> After(DateTimeOffset after);
        ISequence<TSequenceItem> Before(DateTimeOffset before);
        ISequence<TSequenceItem> Between(DateTimeOffset after, DateTimeOffset before);
        Task<T> ToObject<T>(T seed, Func<TSequenceItem, T, T> merge);
        Task<T> ToObject<T>(Func<TSequenceItem, T, T> merge);
    }
}