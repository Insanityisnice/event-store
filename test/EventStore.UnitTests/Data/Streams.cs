using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.Domain.Model;
using Newtonsoft.Json.Linq;

namespace EventStore.UnitTests.Data
{
    public static class Streams
    {
        public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> items)
        {
            foreach(var item in items) yield return await Task.FromResult(item);
        }

        public static IEnumerable<Event> Aggregate_1 = new List<Event>() {
            new Event(Guid.Parse("A0000001-0000-0000-0000-E00000000001"),
                      "Aggregate.Created", "100000003", 1, new DateTimeOffset(new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc)), 
                      JObject.Parse("{ 'id': '1', 'name': 'New Aggregate', 'description': 'Some Description' }")),
            new Event(Guid.Parse("A0000001-0000-0000-0000-E00000000002"),
                      "Aggregate.User.Added", "100000003", 2, new DateTimeOffset(new DateTime(2021, 1, 2, 0, 0, 0, DateTimeKind.Utc)), 
                      JObject.Parse("{ 'id': '1', 'user': '000000001' }")),
            new Event(Guid.Parse("A0000001-0000-0000-0000-E00000000003"),
                      "Aggregate.User.Added", "100000003", 3, new DateTimeOffset(new DateTime(2021, 1, 3, 0, 0, 0, DateTimeKind.Utc)), 
                      JObject.Parse("{ 'id': '1', 'user': '000000002' }")),
            new Event(Guid.Parse("A0000001-0000-0000-0000-E00000000004"),
                      "Aggregate.Closed", "100000003", 4, new DateTimeOffset(new DateTime(2021, 1, 4, 0, 0, 0, DateTimeKind.Utc)), 
                      JObject.Parse("{ 'id': '1' }"))
        };
    }
}