using System.Linq;
using System.Threading.Tasks;
using EventStore.AcceptanceTests.Data;
using TechTalk.SpecFlow;

namespace EventStore.AcceptanceTests.StepDefinitions
{
    [Binding, Scope(Tag = "streams"), Scope(Tag = "create_stream")]
    public class CreateStreamStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;

        public CreateStreamStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [When(@"(.*) is published"), When(@"(.*) are published")]
        public async Task the_events_are_published(string eventLookup)
        {
            var stream = scenarioContext.Get<IEventStream>("stream");
            await stream.Publish(EventData.Events(eventLookup).ToArray());
        }
    }
}