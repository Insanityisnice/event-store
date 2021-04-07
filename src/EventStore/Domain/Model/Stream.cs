namespace EventStore.Domain.Model
{
    public class Stream
    {
        public string StreamName { get; private set; }
        public int Revision { get; private set; }

        internal Stream(string streamName, int revision)
        {
            if (string.IsNullOrWhiteSpace(streamName)) throw new System.ArgumentException($"'{nameof(streamName)}' cannot be null or whitespace.", nameof(streamName));

            StreamName = streamName;
            Revision = revision;
        }
    }
}