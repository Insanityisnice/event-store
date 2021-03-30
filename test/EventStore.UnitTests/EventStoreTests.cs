using System;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace EventStore.UnitTests
{
    [TestClass, TestCategory("EventStore"), TestCategory("EventStoreCore")]
    public class EventStoreTests
    {
        private IServiceProvider provider;
        private ILogger<EventStore> logger;

        [TestInitialize]
        public void TestInitialize()
        {
            logger = Substitute.For<ILogger<EventStore>>();
            provider = Substitute.For<IServiceProvider>();

            provider.GetService(typeof(ILogger<EventStreams>)).Returns(Substitute.For<ILogger<EventStreams>>());
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStore_Construct_Null_Provider()
        {
            provider = null;
            logger = null;

            Action act = () => new EventStore(provider, logger);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStore_Construct_Null_Logger()
        {
            logger = null;

            Action act = () => new EventStore(provider, logger);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStore_Construct()
        {
            var target = new EventStore(provider, logger);

            target.Should().NotBeNull();
        }

        [TestMethod, TestCategory("Streams")]
        public void EventStore_Streams_Successfull_Creation()
        {
            var target = new EventStore(provider, logger);
            
            var streams = target.Streams;
            streams.Should().NotBeNull();
        }

        [TestMethod, TestCategory("Streams")]
        public void EventStore_Streams_Successfull_Creation__Ensure_Singleton()
        {
            var target = new EventStore(provider, logger);
            
            var streams = target.Streams;

            var streamsAgain = target.Streams;
            streamsAgain.Should().BeSameAs(streams);
        }
    }
}
