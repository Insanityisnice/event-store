using System;
using EventStore.Domain.Model;
using Newtonsoft.Json.Linq;

namespace EventStore.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventStore = new EventStore();
            var eventStreams = eventStore.Connect();

            var stream = eventStreams["Entity-1"];
            stream.Add(new Event(Guid.NewGuid(), 
                "Entity.Created", 
                JObject.FromObject(new { Id = 1, Name = "Entity 1" }).ToString(),
                1, DateTimeOffset.UtcNow)
            );
        }
    }
}
