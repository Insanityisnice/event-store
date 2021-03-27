using System;
using EventStore.Persistence;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace EventStore.UnitTests
{
    [TestClass, TestCategory("EventStore"), TestCategory("EventStreams")]
    public class EventStreamsTests
    {
        private IStreamStore streamStore;
        private IServiceProvider provider;
        private ILogger<EventStreams> logger;

        [TestInitialize]
        public void TestInitialize()
        {
            streamStore = Substitute.For<IStreamStore>();
            logger = Substitute.For<ILogger<EventStreams>>();
            provider = Substitute.For<IServiceProvider>();

            provider.GetService(typeof(ILogger<EventStream>)).Returns(Substitute.For<ILogger<EventStream>>());
            provider.GetService(typeof(IStreamStore)).Returns(streamStore);
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStreams_Construct_Null_Provider()
        {
            provider = null;
            logger = null;

            Action act = () => new EventStreams(provider, logger);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStreams_Construct_Null_Logger()
        {
            logger = null;

            Action act = () => new EventStreams(provider, logger);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStreams_Construct()
        {
            var streams = new EventStreams(provider, logger);

            streams.Should().NotBeNull();
        }

        [TestMethod, TestCategory("Stream")]
        public void EventStreams_Stream()
        {
            var streams = new EventStreams(provider, logger);

            var stream = streams["Aggregate-1"];
            stream.Should().NotBeNull();
        }

        [TestMethod, TestCategory("Stream")]
        public void EventStreams_Stream_EnsureUnique()
        {
            var streams = new EventStreams(provider, logger);

            var stream = streams["Aggregate-1"];
            stream.Should().NotBeNull();

            var anotherStream = streams["Aggregate-1"];
            anotherStream.Should().NotBeSameAs(stream);
        }
    }
}
