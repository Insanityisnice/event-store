using Microsoft.Extensions.Logging;

namespace EventStore
{
    internal static class EventIds
    {
        public static class Debug //10000 -> 19999
        {
            public static EventId StreamConstructed = new EventId(10000, "EventStreams.Stream.Constructed");
        }

        public static class Informational
        {
        }
    }
}