namespace EventStore
{
    public interface IEventStore
    {
        IEventStreams Streams { get; }
    }
}