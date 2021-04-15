namespace EventStore.AcceptanceTests.StepDefinitions
{
    public class StreamTestContext
    {
        public string StreamName { get; set; }
        public IEventStream Stream { get; set; }
    }
}