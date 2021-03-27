using System;
using EventStore.Persistence;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace EventStore.UnitTests
{
    [TestClass, TestCategory("EventStore"), TestCategory("EventStream")]
    public class EventStreamTests
    {
        private IStreamStore streamStore;
        private ILogger<EventStream> logger;

        [TestInitialize]
        public void TestInitialize()
        {
            streamStore = Substitute.For<IStreamStore>();
            logger = Substitute.For<ILogger<EventStream>>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStream_Construct_Null_StreamName()
        {
            string streamName = null;
            streamStore = null;
            logger = null;

            Action act = () => new EventStream(streamName, streamStore, logger);

            act.Should().Throw<ArgumentException>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStream_Construct_Empty_StreamName()
        {
            string streamName = "";
            streamStore = null;
            logger = null;

            Action act = () => new EventStream(streamName, streamStore, logger);

            act.Should().Throw<ArgumentException>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStream_Construct_Null_StreamStore()
        {
            string streamName = "Aggregate-1";
            streamStore = null;
            logger = null;

            Action act = () => new EventStream(streamName, streamStore, logger);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStream_Construct_Null_Logger()
        {
            string streamName = "Aggregate-1";
            logger = null;

            Action act = () => new EventStream(streamName, streamStore, logger);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod, TestCategory("Constructor")]
        public void EventStream_Construct()
        {
            string streamName = "Aggregate-1";
            var target = new EventStream(streamName, streamStore, logger);
        }
    }
}
