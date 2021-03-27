using Microsoft.Extensions.Logging;
using System;

namespace EventStore
{
    internal static class LogMessages
    {
        #region Method Scope
        private static readonly Func<ILogger, string, string, IDisposable> methodScope = LoggerMessage.DefineScope<string, string>("{class}.{method}");
        public static IDisposable MethodScope(this ILogger logger, string @class, string method) => methodScope(logger, @class, method);
        #endregion Method Scope

        #region EventStreams Messages
        private static readonly Action<ILogger, string, Exception> streamConstructed = LoggerMessage.Define<string>(LogLevel.Debug, 
                EventIds.Debug.StreamConstructed, "Constructed EventStream '{streamName}'.");
        public static void StreamConstructed(this ILogger logger, string streamName) => streamConstructed(logger, streamName, null);
        #endregion EventStreams Messages

        #region Added Events
        private static readonly Action<ILogger, int, string, Exception> addEvents = LoggerMessage.Define<int, string>(LogLevel.Information, 
                new EventId(0, "EventStore.Stream.Added.Events"), "Added {count} events to the '{streamName}' stream.");
        public static void AddedEvents(this ILogger logger, string streamName, int count) => addEvents(logger, count, streamName, null);
        #endregion Added Events
    }
}