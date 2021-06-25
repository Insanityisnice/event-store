using System.Linq;
using System.Threading.Tasks;
using EventStore.AcceptanceTests.Data;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace EventStore.AcceptanceTests.StepDefinitions
{
    [Binding, Scope(Tag = "streams"), Scope(Tag = "create_stream")]
    public class CreateStreamStepDefinitions
    {
        private readonly StreamTestContext context;

        public CreateStreamStepDefinitions(StreamTestContext context)
        {
            this.context = context;
        }

        [When(@"'(.*)' are published")]
        public async Task the_events_are_published(string eventLookup)
        {
            context.Stream.Should().NotBeNull("the stream should already be setup.");
            await context.Stream.Publish(EventData.Events(eventLookup).ToArray());
        }
    }
}