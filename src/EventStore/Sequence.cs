using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventStore
{
    internal abstract class Sequence<TSequenceItem> : ISequence<TSequenceItem>
    {
        protected DateTimeOffset after = default(DateTimeOffset);
        protected DateTimeOffset before = default(DateTimeOffset);

        public ISequence<TSequenceItem> After(DateTimeOffset after)
        {
            this.after = after;
            return this;
        }

        public ISequence<TSequenceItem> Before(DateTimeOffset before)
        {
            this.before = before;
            return this;
        }

        public ISequence<TSequenceItem> Between(DateTimeOffset after, DateTimeOffset before)
        {
            After(after);
            Before(this.before = before);
            return this;
        }

        public abstract IAsyncEnumerator<TSequenceItem> GetAsyncEnumerator(CancellationToken cancellationToken);

        public abstract Task<T> ToObject<T>(T seed, Func<TSequenceItem, T, T> merge);

        public Task<T> ToObject<T>(Func<TSequenceItem, T, T> merge)
        {
            return ToObject<T>(default(T), merge);
        }
    }
}