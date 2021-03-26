class program
{
    void main()
    {
        IEventStore eventStore = new EventStore();
        IEventStreams eventStreams = eventStore.Connect();
        
        var stream = eventStreams["aggregate-1"];

        eventStreams.Categories();
        eventStreams.StreamsInCategory("aggregate");


        stream.Add(events);
        stream.ToObject<Engagement>((event, engagement) => engagement.Apply(event));
        stream.After(new DateTimeOffset("June 3, 2020")).ToObject<Engagement>(...);
        stream.Before(new DateTimeOffset("June 3, 2020")).ToObject<Engagement>(...);
        stream.After(...).Before(...);
        stream.Between(new DateTimeOffset("June 3, 2020"), new DateTimeOffset("June 5, 2020"));

        stream.Snapshots.Add(engagement);
        stream.Snapshots.Current();
        stream.Snapshots.After();
        stream.Snapshots.Before();
        stream.Snapshots.Between();
    }
}