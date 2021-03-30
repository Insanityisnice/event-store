using Microsoft.Extensions.Logging;

namespace EventStore
{
    internal static class EventIds
    {
        public static class Debug //10000 -> 10999
        {
            public static EventId EventStreams_StreamConstructed = new EventId(10000, "EventStreams.Stream.Constructed");
        }

        public static class Information // 1000 - 1499
        {
            public static EventId EventStream_EventsAdded = new EventId(1000, "EventStream.Events.Added");
        }

        public static class Warning // 500 - 999
        {
            public static EventId EventStream_AttemptToAddNullEvents = new EventId(500, "EventStream.Null.Events.Add.Attempted");
            public static EventId EventStream_AttemptToAddEmptyEvents = new EventId(501, "EventStream.Empty.Events.Add.Attempted");
        }

        public static class Error // 100 - 499
        {

        }
    }
}