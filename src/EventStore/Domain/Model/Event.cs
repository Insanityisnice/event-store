using System;
using Newtonsoft.Json.Linq;

namespace EventStore.Domain.Model
{
    public class Event : ISequenceItem
    {
        public Guid Id { get; private set; }
        public String Type { get; private set; }
        public String User { get; private set; }
        public Int64 SequenceNumber { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
        public JObject Data { get; private set; }
        
        internal Event(Guid id, string type, string user, long sequenceNumber, DateTimeOffset timestamp, JObject data)
        {
            Id = id;
            Type = type;
            User = user;
            SequenceNumber = sequenceNumber;
            Timestamp = timestamp;
            Data = data;
        }

        protected Event(Event @event, long sequenceNumber)
            : this(@event.Id, @event.Type, @event.User, sequenceNumber, @event.Timestamp, @event.Data)
        {
        }

        public Event(Guid id, string type, string user, DateTimeOffset timestamp, JObject data)
            : this(id, type, user, -1, timestamp, data)
        {
        }

        public Event(Guid id, string type, string user, JObject data)
            : this(id, type, user, DateTimeOffset.UtcNow, data)
        {
        }
    }
}