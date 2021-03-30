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
                EventIds.Debug.EventStreams_StreamConstructed, "Constructed EventStream '{streamName}'.");
        public static void StreamConstructed(this ILogger logger, string streamName) => streamConstructed(logger, streamName, null);
        #endregion EventStreams Messages

        #region EventStream Messages
        private static readonly Action<ILogger, string, Exception> attemptToAddNullEvents = LoggerMessage.Define<string>(LogLevel.Warning, 
                EventIds.Warning.EventStream_AttemptToAddNullEvents, "An null events array was supplied for stream '{streamName}'.");
        public static void AttemptToAddNullEvents(this ILogger logger, string streamName) => attemptToAddNullEvents(logger, streamName, null);

        private static readonly Action<ILogger, string, Exception> attemptToAddEmptyEvents = LoggerMessage.Define<string>(LogLevel.Warning, 
                EventIds.Warning.EventStream_AttemptToAddEmptyEvents, "An empty events array was supplied for stream '{streamName}'.");
        public static void AttemptToAddEmptyEvents(this ILogger logger, string streamName) => attemptToAddEmptyEvents(logger, streamName, null);
        
        private static readonly Action<ILogger, int, string, Exception> addEvents = LoggerMessage.Define<int, string>(LogLevel.Information, 
                EventIds.Information.EventStream_EventsAdded, "Added {count} events to the '{streamName}' stream.");
        public static void AddedEvents(this ILogger logger, string streamName, int count) => addEvents(logger, count, streamName, null);
        #endregion EventStream Messages
    }
}