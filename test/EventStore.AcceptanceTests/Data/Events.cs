using System;
using System.Collections.Generic;
using System.Linq;
using EventStore.Domain.Model;
using Newtonsoft.Json.Linq;

namespace EventStore.AcceptanceTests.Data
{
    internal static class EventData
    {
        private static DateTimeOffset NovemberFirst2019 = new DateTimeOffset(2019, 11, 1, 0, 0, 0, TimeSpan.FromHours(0));
        private static DateTimeOffset YearEnd2019 = new DateTimeOffset(2019, 12, 31, 23, 59, 59, TimeSpan.FromHours(0));
        private static DateTimeOffset JanuaryFirst2020 = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.FromHours(0));
        private static DateTimeOffset EndOfMarch2020 = new DateTimeOffset(2020, 3, 31, 23, 59, 59, TimeSpan.FromHours(0));
        private static DateTimeOffset BeginingOfApril2020 = new DateTimeOffset(2020, 4, 1, 0, 0, 0, TimeSpan.FromHours(0));
        

        private static IDictionary<string, IEnumerable<Event>> events = 
            new Dictionary<string, IEnumerable<Event>>() {
#region Aggregate-1
                { 
                    "Published.Existing.Aggregate-1", 
                    new List<Event>() {
                        new Event(new Guid("00000000-0000-0000-0001-E00000000001"), "Aggregate.Created", "000000001", 
                            JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" }))
                    }
                },
                { 
                    "Published.Existing.SingleEvent", 
                    new List<Event>() {
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0001-E00000000001"), "Aggregate.Created", "000000001", 
                                JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })),
                            0
                        )                          
                    }
                },
#endregion Aggregate-1
#region Aggregate-2
                { 
                    "Published.Existing.Aggregate-2", 
                    new List<Event>() {
                        new Event(new Guid("00000000-0000-0000-0002-E00000000001"), "Aggregate.Created", "000000001", 
                            JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000002" })),
                        new Event(new Guid("00000000-0000-0000-0002-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000002", Attr1 = "01" }))
                    }
                },
                { 
                    "Published.Existing.TwoEvents", 
                    new List<Event>() {
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0002-E00000000001"), "Aggregate.Created", "000000001", 
                                JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })), 0
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0002-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000002", Attr1 = "01" })), 1
                        )                        
                    }
                },
#endregion Aggregate-2
#region Aggregate-3
                { 
                    "Published.Existing.Aggregate-3", 
                    new List<Event>() {
                        new Event(new Guid("00000000-0000-0000-0003-E00000000001"), "Aggregate.Created", "000000001", 
                            NovemberFirst2019 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })),
                        new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            YearEnd2019 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr1 = "01" })),
                        new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            JanuaryFirst2020, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr2 = "02" })),
                        new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Name.Set", "000000001", 
                            EndOfMarch2020 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Name = "Aggregate-3" })),
                        new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            BeginingOfApril2020 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr1 = "01" }))
                    }
                },
                { 
                    "Published.Existing.ManyEvents", 
                    new List<Event>() {
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000001"), "Aggregate.Created", "000000001", 
                            NovemberFirst2019 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })), 0
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            YearEnd2019 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr1 = "01" })), 1
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            JanuaryFirst2020, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr2 = "02" })), 2
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Name.Set", "000000001", 
                            EndOfMarch2020 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Name = "Aggregate-3" })), 3
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            BeginingOfApril2020 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr1 = "01" })), 4
                        )
                    }
                },
#region After
                { 
                    "Published.Existing.After_Begining_Of_A_Year", 
                    new List<Event>() {                        
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            JanuaryFirst2020, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr2 = "02" })), 2
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Name.Set", "000000001", 
                            EndOfMarch2020 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Name = "Aggregate-3" })), 3
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            BeginingOfApril2020 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr1 = "01" })), 4
                        )
                    }
                },
                { 
                    "Published.Existing.After_End_Of_A_Year", 
                    new List<Event>() {
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            YearEnd2019 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr1 = "01" })), 1
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            JanuaryFirst2020, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr2 = "02" })), 2
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Name.Set", "000000001", 
                            EndOfMarch2020 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Name = "Aggregate-3" })), 3
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            BeginingOfApril2020 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr1 = "01" })), 4
                        )
                    }
                },
                { 
                    "Published.Existing.After_Begining_Of_A_Month", 
                    new List<Event>() {
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            BeginingOfApril2020 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr1 = "01" })), 4
                        )
                    }
                },
                { 
                    "Published.Existing.After_End_Of_A_Month", 
                    new List<Event>() {
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Name.Set", "000000001", 
                            EndOfMarch2020 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Name = "Aggregate-3" })), 3
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0003-E00000000002"), "Aggregate.Attribute.Set", "000000001", 
                            BeginingOfApril2020 ,JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001", Attr1 = "01" })), 4
                        )
                    }
                },
#endregion After
#endregion Aggregate-3
#region Single.OnlyRequiredProperties
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
#endregion Single.OnlyRequiredProperties
#region Single.AllProperties
                { 
                    "ToPublish.Single.AllProperties", 
                    new List<Event>() {
                        new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                            JanuaryFirst2020, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000002" }))
                    }
                },
                { 
                    "Published.Single.AllProperties", 
                    new List<Event>() {
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                                JanuaryFirst2020, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })),
                            0
                        )                            
                    }
                },
#endregion Single.AllProperties
#region Multiple.OnlyRequiredProperties
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
#endregion Multiple.OnlyRequiredProperties
#region Multiple.AllProperties
                { 
                    "ToPublish.Multiple.AllProperties", 
                    new List<Event>() {
                        new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                            JanuaryFirst2020, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })),
                        new Event(new Guid("00000000-0000-0000-0000-E00000000002"), "Aggregate.Property.Set", "000000001", 
                            JanuaryFirst2020, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000002" }))
                    }
                },
                { 
                    "Published.Multiple.AllProperties", 
                    new List<Event>() {
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0000-E00000000001"), "Aggregate.Created", "000000001", 
                                JanuaryFirst2020, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000001" })),
                            0
                        ),
                        new HydratedEvent(
                            new Event(new Guid("00000000-0000-0000-0000-E00000000002"), "Aggregate.Property.Set", "000000001", 
                                JanuaryFirst2020, JObject.FromObject(new { Id = "00000000-0000-0000-0000-E00000000002" })),
                            1
                        )                           
                    }
                },
#endregion Multiple.AllProperties
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