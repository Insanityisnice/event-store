using System;
using System.Threading.Tasks;
using EventStore.AcceptanceTests.Drivers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace EventStore.AcceptanceTests.StepDefinitions
{
    [Binding, Scope(Tag = "streams")]
    public class CommonStepDefinitions
    {
        private readonly StreamStoreDriver storeDriver;

        public CommonStepDefinitions(StreamStoreDriver storeDriver)
        {
            this.storeDriver = storeDriver ?? throw new ArgumentNullException(nameof(storeDriver));
        }

        [Given(@"(.*) does not exist")]
        public async Task stream_does_not_exist(string stream)
        {
            (await storeDriver.Exists(stream)).Should().BeFalse();
        }

        [Given(@"(.*) does exist")]
        public async Task stream_does_exist(string stream)
        {
            (await storeDriver.Exists(stream)).Should().BeTrue();
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