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

        [Given(@"(.*) does not exist")]
        public async Task stream_does_not_exist(string streamName)
        {
            if (String.IsNullOrWhiteSpace(context.StreamName)) context.StreamName = streamName;
            streamName.Should().Be(context.StreamName);

            var exists = await storeDriver.Exists(context.StreamName);
            exists.Should().BeFalse();
            
            if (context.Stream == null) context.Stream = streamsDriver.GetStream(context.StreamName);
        }

        [Given(@"(.*) does exist")]
        public async Task stream_does_exist(string streamName)
        {
            if (String.IsNullOrWhiteSpace(context.StreamName)) context.StreamName = streamName;
            streamName.Should().Be(context.StreamName);

            var exists = await storeDriver.Exists(context.StreamName);
            exists.Should().BeTrue();

            if (context.Stream == null) context.Stream = streamsDriver.GetStream(context.StreamName);
        }

        [Then(@"(.*) contains no events")]
        public async Task contains_no_events(string streamName)
        {
            await storeDriver.StreamHasNoEvents(streamName);
        }

        [Then(@"(.*) contains (.*)")]
        public async Task contains_published_events(string streamName, string eventsLookup)
        {
            await storeDriver.StreamHasExpectedEvents(streamName, EventData.Events(eventsLookup));
        }
    }
}