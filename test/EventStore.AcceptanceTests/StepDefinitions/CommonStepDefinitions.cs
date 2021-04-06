using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace EventStore.AcceptanceTests.StepDefinitions
{
    [Binding, Scope(Tag = "streams")]
    public class CommonStepDefinitions
    {
        public CommonStepDefinitions(FeatureContext featureContext)
        {
        }

        [Given(@"(.*) does not exist")]
        public async Task stream_does_not_exist(string stream)
        {
            await Task.CompletedTask;
            ScenarioContext.StepIsPending();
        }

        [Given(@"(.*) does exist")]
        public async Task stream_does_exist(string stream)
        {
            await Task.CompletedTask;
            ScenarioContext.StepIsPending();
        }

        [Then(@"(.*) contains no events")]
        public async Task contains_no_events()
        {
            await Task.CompletedTask;
            ScenarioContext.StepIsPending();

        }

        [Then(@"(.*) contains (.*)")]
        public async Task contains_published_events(string stream, string eventsLookup)
        {
            await Task.CompletedTask;
            ScenarioContext.StepIsPending();
        }
    }
}