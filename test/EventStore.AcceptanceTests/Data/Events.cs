using System;
using System.Collections.Generic;
using System.Linq;
using EventStore.Domain.Model;
using Newtonsoft.Json.Linq;

namespace EventStore.AcceptanceTests.Data
{
    internal static class EventData
    {
        private static DateTimeOffset JanuaryFirst = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.FromHours(0));

        private static IDictionary<string, IEnumerable<Event>> events = 
            new Dictionary<string, IEnumerable<Event>>() {
                { 
                    "Published.Existing.Aggregate-1", 
                    new List<Event>() {
                        new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                            JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" }))
                    }
                },
                { 
                    "ToPublish.Single.OnlyRequiredProperties", 
                    new List<Event>() {
                        new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                            JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" }))
                    }
                },
                { 
                    "Published.Single.OnlyRequiredProperties", 
                    new List<Event>() {
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                                JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })),
                            0
                        )                            
                    }
                },
                { 
                    "ToPublish.Single.AllProperties", 
                    new List<Event>() {
                        new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                            JanuaryFirst, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000002" }))
                    }
                },
                { 
                    "Published.Single.AllProperties", 
                    new List<Event>() {
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                                JanuaryFirst, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })),
                            0
                        )                            
                    }
                },
                { 
                    "ToPublish.Multiple.OnlyRequiredProperties", 
                    new List<Event>() {
                        new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                            JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })),
                        new Event(new Guid("00000000-0000-0000-0000-E00000000002"), "Aggregate.Property.Set", "000000001", 
                            JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000002" }))
                    }
                },
                { 
                    "Published.Multiple.OnlyRequiredProperties", 
                    new List<Event>() {
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                                JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })),
                            0
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0000-E00000000002"), "Aggregate.Property.Set", "000000001", 
                                JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000002" })),
                            1
                        )                           
                    }
                },
                { 
                    "ToPublish.Multiple.AllProperties", 
                    new List<Event>() {
                        new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                            JanuaryFirst, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })),
                        new Event(new Guid("00000000-0000-0000-0000-E00000000002"), "Aggregate.Property.Set", "000000001", 
                            JanuaryFirst, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000002" }))
                    }
                },
                { 
                    "Published.Multiple.AllProperties", 
                    new List<Event>() {
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                                JanuaryFirst, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })),
                            0
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0000-E00000000002"), "Aggregate.Property.Set", "000000001", 
                                JanuaryFirst, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000002" })),
                            1
                        )                           
                    }
                }
            };
        
        public static IEnumerable<Event> Events(string key)
        {
            IEnumerable<Event> result = Enumerable.Empty<Event>();
            events.TryGetValue(key, out result);

            return result;
        }

        private class HydratedEvent : Event
        {
            public HydratedEvent(Event @event, int sequenceNumber)
                : base(@event, sequenceNumber) {}
        }
    }
}