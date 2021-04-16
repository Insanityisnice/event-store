namespace EventStore.Domain.Model
{
    public abstract class Stream
    {
        public string StreamName { get; private set; }
        public int Revision { get; protected set; }

        protected Stream(string streamName, int revision)
        {
            if (string.IsNullOrWhiteSpace(streamName)) throw new System.ArgumentException($"'{nameof(streamName)}' cannot be null or whitespace.", nameof(streamName));

            StreamName = streamName;
            Revision = revision;
        }
    }
}