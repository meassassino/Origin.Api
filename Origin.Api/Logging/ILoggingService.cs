

// ReSharper disable UnusedTypeParameter

namespace Origin.Api.Logging
{
    /// <summary>
    /// Defines methods to produce different levels of logging
    /// </summary>
    /// <typeparam name="T">The type who's name is used for the logger category name.</typeparam>
    public interface ILoggingService<T>
    {
        /// <summary>
        /// Creates a log entry for when an operation starts.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        void LogOperationStart(string containerName, string methodName, string correlationId);

        /// <summary>
        /// Creates a log entry for when an operation starts.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        void LogOperationEnd(string containerName, string methodName, string correlationId);

        /// <summary>
        /// Logs a mediator send operation.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="response">The response.</param>
        void LogMediatorSend(string containerName, string methodName, string correlationId, object response);

        /// <summary>
        ///     Logs a mediator send operation log level 'Debug'
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="response">The response.</param>
        void LogMediatorSendDebug(string containerName, string methodName, string correlationId, object response);

        /// <summary>
        ///     Creates a 'Debug' log entry.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        void LogOperationStartDebug(string containerName, string methodName, string correlationId);

        /// <summary>
        ///     Creates a 'Debug' log entry.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        void LogOperationEndDebug(string containerName, string methodName, string correlationId);

        /// <summary>
        ///     Creates a 'Debug' log entry.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="correlationId"></param>
        /// <param name="request"></param>
        void LogDebug(string message, string correlationId, object request = null);

        /// <summary>
        ///     Creates a 'Debug' exception log entry.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="correlationId"></param>
        void LogDebug(Exception exception, string correlationId);

        /// <summary>
        /// Creates a 'Info' log entry.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="correlationId"></param>
        /// <param name="request"></param>
        void LogInfo(string message, string correlationId, object request = null);

        /// <summary>
        /// Creates a 'Warning' log entry.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="correlationId"></param>
        /// <param name="request"></param>
        void LogWarning(string message, string correlationId, object request = null);

        /// <summary>
        /// Creates a 'Error' log entry.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="correlationId"></param>
        void LogError(Exception exception, string correlationId);

        /// <summary>
        /// Creates a 'Critical' log entry.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="correlationId"></param>
        /// <param name="request"></param>
        void LogCritical(Exception exception, string correlationId, object request = null);

        /// <summary>
        /// Creates a 'Error' log entry.
        /// </summary>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="request">The request.</param>
        void LogError(string exceptionMessage, string correlationId, object request = null);
    }
}
