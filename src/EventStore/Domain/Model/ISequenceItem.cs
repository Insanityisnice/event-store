using System;

namespace EventStore.Domain.Model
{
    public interface ISequenceItem
    {
        Int64 SequenceNumber { get; }
        DateTimeOffset Timestamp { get; }
    }
}