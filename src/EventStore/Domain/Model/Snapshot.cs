using System;

namespace EventStore.Domain.Model
{
    public class Snapshot : ISequenceItem
    {
        public Guid Id { get; private set; }
        public String Data { get; private set; }
        public Int64 SequenceNumber { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
    }
}