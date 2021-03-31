using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApprovalTests;
using EventStore.Domain.Model;
using EventStore.Persistence;
using EventStore.UnitTests.Data;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NSubstitute;

namespace EventStore.UnitTests
{
    [TestClass, TestCategory("EventStore"), TestCategory("EventStream")]
    public class EventStreamTests
    {
        private IStreamStore streamStore;
        private ILogger<EventStream> logger;

        [TestInitialize]
        public void TestInitialize()
        {
            streamStore = Substitute.For<IStreamStore>();
            logger = Substitute.For<ILogger<EventStream>>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStream_Construct_Null_StreamName()
        {
            string streamName = null;
            streamStore = null;
            logger = null;

            Action act = () => new EventStream(streamName, streamStore, logger);

            act.Should().Throw<ArgumentException>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStream_Construct_Empty_StreamName()
        {
            string streamName = "";
            streamStore = null;
            logger = null;

            Action act = () => new EventStream(streamName, streamStore, logger);

            act.Should().Throw<ArgumentException>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStream_Construct_Null_StreamStore()
        {
            string streamName = "Aggregate-1";
            streamStore = null;
            logger = null;

            Action act = () => new EventStream(streamName, streamStore, logger);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStream_Construct_Null_Logger()
        {
            string streamName = "Aggregate-1";
            logger = null;

            Action act = () => new EventStream(streamName, streamStore, logger);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStream_Construct()
        {
            string streamName = "Aggregate-1";
            var target = new EventStream(streamName, streamStore, logger);
        }

        [TestMethod, TestCategory("Add")]
        public void EventStream_Add__Null_Events()
        {
            string streamName = "Aggregate-1";
            var target = new EventStream(streamName, streamStore, logger);

            Func<Task> act = async () => await target.Add(null); //Enumerable.Empty<Event>().ToArray()
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod, TestCategory("Add")]
        public async Task EventStream_Add__Single_Event()
        {
            string streamName = "Aggregate-1";
            var target = new EventStream(streamName, streamStore, logger);

            await target.Add(Streams.Aggregate_1.First());
            
            await streamStore.Received().AddEventsToStream(streamName, Arg.Is<IEnumerable<Event>>(a => a.First() == Streams.Aggregate_1.First()));
        }

        [TestMethod, TestCategory("Add")]
        public async Task EventStream_Add__Multiple_Events()
        {
            string streamName = "Aggregate-1";
            var target = new EventStream(streamName, streamStore, logger);

            await target.Add(Streams.Aggregate_1.Skip(1).Take(2).ToArray());
            
            await streamStore.Received().AddEventsToStream(streamName, Arg.Is<IEnumerable<Event>>(a => a.Except(Streams.Aggregate_1.Skip(1).Take(2)).Count() == 0));
        }

        [TestMethod, TestCategory("ToObject")]
        public async Task EventStream_ToObject__Empty_Set_of_Events()
        {
            string streamName = "Aggregate-1";
            int numOfEvents = 0;
            var target = new EventStream(streamName, streamStore, logger);

            streamStore.ReadStream(streamName).Returns(Streams.Aggregate_1.Take(numOfEvents).ToAsyncEnumerable());
            var result = await target.ToObject<Aggregate>(Substitute.ForPartsOf<Aggregate>(), (@event, aggregate) => aggregate.Apply(@event));

            result.ReceivedCalls().Count().Should().Be(numOfEvents);
        }

        [TestMethod, TestCategory("ToObject")]
        public async Task EventStream_ToObject__Only_one_event()
        {
            string streamName = "Aggregate-1";
            int numOfEvents = 1;
            var target = new EventStream(streamName, streamStore, logger);

            streamStore.ReadStream(streamName).Returns(Streams.Aggregate_1.Take(numOfEvents).ToAsyncEnumerable());
            var result = await target.ToObject<Aggregate>(Substitute.ForPartsOf<Aggregate>(), (@event, aggregate) => aggregate.Apply(@event));

            result.ReceivedCalls().Count().Should().Be(numOfEvents);

            Approvals.Verify(JObject.FromObject(result).ToString());
        }

        [TestMethod, TestCategory("ToObject")]
        public async Task EventStream_ToObject__All_events()
        {
            string streamName = "Aggregate-1";
            int numOfEvents = 4;
            var target = new EventStream(streamName, streamStore, logger);

            streamStore.ReadStream(streamName).Returns(Streams.Aggregate_1.Take(numOfEvents).ToAsyncEnumerable());
            var result = await target.ToObject<Aggregate>(Substitute.ForPartsOf<Aggregate>(), (@event, aggregate) => aggregate.Apply(@event));

            result.ReceivedCalls().Count().Should().Be(numOfEvents);

            Approvals.Verify(JObject.FromObject(result).ToString());
        }

        [TestMethod, TestCategory("ToObject")]
        public async Task EventStream_ToObject__Before_specified_date()
        {
            string streamName = "Aggregate-1";
            int numOfEvents = 2;
            DateTimeOffset from = default(DateTimeOffset);
            DateTimeOffset to = new DateTimeOffset(new DateTime(2021, 1, 2, 23, 59, 59, DateTimeKind.Utc));
            var target = new EventStream(streamName, streamStore, logger);

            streamStore.ReadStream(streamName, from, to).Returns(Streams.Aggregate_1.Take(numOfEvents).ToAsyncEnumerable());
            var result = await target.Before(to).ToObject<Aggregate>(Substitute.ForPartsOf<Aggregate>(), (@event, aggregate) => aggregate.Apply(@event));

            result.ReceivedCalls().Count().Should().Be(numOfEvents);

            Approvals.Verify(JObject.FromObject(result).ToString());
        }

        [TestMethod, TestCategory("ToObject")]
        public async Task EventStream_ToObject__After_specified_date()
        {
            string streamName = "Aggregate-1";
            int numOfEvents = 2;
            DateTimeOffset from = new DateTimeOffset(new DateTime(2021, 1, 2, 23, 59, 59, DateTimeKind.Utc));
            DateTimeOffset to = default(DateTimeOffset);
            var target = new EventStream(streamName, streamStore, logger);

            streamStore.ReadStream(streamName, from, to).Returns(Streams.Aggregate_1.Skip(2).Take(numOfEvents).ToAsyncEnumerable());
            var result = await target.After(from).ToObject<Aggregate>(Substitute.ForPartsOf<Aggregate>(), (@event, aggregate) => aggregate.Apply(@event));

            result.ReceivedCalls().Count().Should().Be(numOfEvents);

            Approvals.Verify(JObject.FromObject(result).ToString());
        }

        [TestMethod, TestCategory("ToObject")]
        public async Task EventStream_ToObject__Between_dates_inclusive()
        {
            string streamName = "Aggregate-1";
            int numOfEvents = 2;
            DateTimeOffset from = new DateTimeOffset(new DateTime(2021, 1, 2, 0, 0, 0, DateTimeKind.Utc));
            DateTimeOffset to = new DateTimeOffset(new DateTime(2021, 1, 3, 0, 0, 0, DateTimeKind.Utc));
            var target = new EventStream(streamName, streamStore, logger);

            streamStore.ReadStream(streamName, from, to).Returns(Streams.Aggregate_1.Skip(1).Take(numOfEvents).ToAsyncEnumerable());
            var result = await target.Between(from, to).ToObject<Aggregate>(Substitute.ForPartsOf<Aggregate>(), (@event, aggregate) => aggregate.Apply(@event));

            result.ReceivedCalls().Count().Should().Be(numOfEvents);

            Approvals.Verify(JObject.FromObject(result).ToString());
        }
    }
}