using System;

namespace EventStore.Domain.Model
{
    public class Event : ISequenceItem
    {
        public Guid Id { get; private set; }
        public String Type { get; private set; }
        public String Data { get; private set; }
        public Int64 SequenceNumber { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }

        public Event(Guid id, string type, string data, long sequenceNumber, DateTimeOffset timestamp)
        {
            Id = id;
            Type = type;
            Data = data;
            SequenceNumber = sequenceNumber;
            Timestamp = timestamp;
        }
    }
}