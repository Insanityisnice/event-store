using System;
using System.Threading.Tasks;
using EventStore.AcceptanceTests.Data;
using EventStore.AcceptanceTests.Drivers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace EventStore.AcceptanceTests.StepDefinitions
{
    [Binding, Scope(Tag = "streams")]
    public class CommonStepDefinitions
    {
        private readonly StreamStoreDriver storeDriver;
        private readonly StreamsDriver streamsDriver;
        private readonly StreamTestContext context;

        public CommonStepDefinitions(StreamStoreDriver storeDriver, StreamsDriver streamsDriver, StreamTestContext context)
        {
            this.storeDriver = storeDriver ?? throw new ArgumentNullException(nameof(storeDriver));
            this.streamsDriver = streamsDriver ?? throw new ArgumentNullException(nameof(streamsDriver));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [Given(@"'([^\']*)' does not exist")]
        public async Task stream_does_not_exist(string streamName)
        {
            if (String.IsNullOrWhiteSpace(context.StreamName)) context.StreamName = streamName;
            streamName.Should().Be(context.StreamName);

            var exists = await storeDriver.Exists(context.StreamName);
            exists.Should().BeFalse();
            
            if (context.Stream == null) context.Stream = streamsDriver.GetStream(context.StreamName);
        }

        [Given(@"'([^\']*)' does exist")]
        public async Task stream_does_exist(string streamName)
        {
            if (String.IsNullOrWhiteSpace(context.StreamName)) context.StreamName = streamName;
            streamName.Should().Be(context.StreamName);

            var exists = await storeDriver.Exists(context.StreamName);
            exists.Should().BeTrue();

            if (context.Stream == null) context.Stream = streamsDriver.GetStream(context.StreamName);
        }

        [Given(@"'([^\']*)' starts with '([^\']*)'")]
        public async Task starts_with_events(string streamName, string eventsLookup)
        {
            await storeDriver.AddEvents(streamName, EventData.Events(eventsLookup));
        }


        [Then(@"'([^\']*)' has no events")]
        public async Task has_no_events(string streamName)
        {
            await storeDriver.StreamHasNoEvents(streamName);
        }

        [Then(@"'([^\']*)' after '([^\']*)' contains '([^\']*)'")]
        public async Task after_contains_published_events(string streamName, DateTimeOffset afterDate, string eventsLookup)
        {
            await storeDriver.StreamHasExpectedEventsAfter(streamName, afterDate, EventData.Events(eventsLookup));
        }

        [Then(@"'([^\']*)' contains '([^\']*)'")]
        public async Task contains_published_events(string streamName, string eventsLookup)
        {
            await storeDriver.StreamHasExpectedEvents(streamName, EventData.Events(eventsLookup));
        }
    }
}