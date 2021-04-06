using System.Collections.Generic;
using System.Linq;
using EventStore.Domain.Model;

namespace EventStore.AcceptanceTests.Data
{
    internal static class EventData
    {
        public static IEnumerable<Event> Events(string key)
        {
            return Enumerable.Empty<Event>();
        }
    }
}