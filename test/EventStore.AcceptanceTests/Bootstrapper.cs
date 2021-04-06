namespace EventStore.AcceptanceTests
{
    public static class Bootstrapper
    {
        public static IEventStore EventStore { get; private set; }

        public static void Initialize() {}
    }
}