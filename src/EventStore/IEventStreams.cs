namespace EventStore
{
    public interface IEventStreams
    {
        IEventStream this[string streamName] { get; }
    }
}