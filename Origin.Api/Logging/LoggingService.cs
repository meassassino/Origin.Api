namespace Origin.Api.Logging
{
    /// <summary>
    /// Generic Implementation of wrapper service for ILogger.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ILoggingService{T}" />
    public class LoggingService<T> : ILoggingService<T>
    {
        private readonly ILogger<T> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingService{T}" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException" />
        public LoggingService(ILogger<T> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public void LogOperationStart(string containerName, string methodName, string correlationId)
        {
            LogInfo($"{containerName}/{methodName} initiated", correlationId);
        }

        /// <inheritdoc />
        public void LogOperationEnd(string containerName, string methodName, string correlationId)
        {
            LogInfo($"{containerName}/{methodName} finished", correlationId);
        }

        /// <inheritdoc />
        public void LogMediatorSend(string containerName, string methodName, string correlationId, object response)
        {
            LogInfo($"{containerName}/{methodName} - mediator send success", correlationId, response);
        }

        /// <inheritdoc />
        public void LogOperationStartDebug(string containerName, string methodName, string correlationId)
        {
            LogDebug($"{containerName}/{methodName} initiated", correlationId);
        }

        /// <inheritdoc />
        public void LogOperationEndDebug(string containerName, string methodName, string correlationId)
        {
            LogDebug($"{containerName}/{methodName} finished", correlationId);
        }

        /// <inheritdoc />
        public void LogMediatorSendDebug(string containerName, string methodName, string correlationId, object response)
        {
            LogDebug($"{containerName}/{methodName} - mediator send success", correlationId, response);
        }

        /// <inheritdoc />
        public void LogDebug(string message, string correlationId, object request = null)
        {
            var logEntry = new LogEntry
            {
                CorrelationId = correlationId,
                InstrumentationDateTime = DateTime.UtcNow,
                Originator = typeof(T).FullName,
                MessageNumber = 1,
                Message = message
            };

            _logger.LogDebug("{Message} {@LogEntry} {@Request}", message, logEntry, request);
        }

        /// <inheritdoc />
        public void LogDebug(Exception exception, string correlationId)
        {
            var logEntry = new LogEntry
            {
                CorrelationId = correlationId,
                InstrumentationDateTime = DateTime.UtcNow,
                Originator = typeof(T).FullName,
                MessageNumber = 1,
                Message = exception.Message
            };

            _logger.LogDebug("{Message} {@LogEntry} {@Exception}", exception.Message, logEntry, exception);
        }

        /// <inheritdoc />
        public void LogError(Exception exception, string correlationId)
        {
            var logEntry = new LogEntry
            {
                CorrelationId = correlationId,
                InstrumentationDateTime = DateTime.UtcNow,
                Originator = typeof(T).FullName,
                MessageNumber = 1,
                Message = exception.Message
            };

            _logger.LogError("{Message} {@LogEntry} {@Exception}", exception.Message, logEntry, exception);
        }

        /// <inheritdoc />
        public void LogError(string exceptionMessage, string correlationId, object request = null)
        {
            var logEntry = new LogEntry
            {
                CorrelationId = correlationId,
                InstrumentationDateTime = DateTime.UtcNow,
                Originator = typeof(T).FullName,
                MessageNumber = 1,
                Message = exceptionMessage
            };

            _logger.LogError("{Message} {@LogEntry} {@Request}", exceptionMessage, logEntry, request);
        }

        /// <inheritdoc />
        public void LogInfo(string message, string correlationId, object request = null)
        {
            var logEntry = new LogEntry
            {
                CorrelationId = correlationId,
                InstrumentationDateTime = DateTime.UtcNow,
                Originator = typeof(T).FullName,
                MessageNumber = 1,
                Message = message
            };

            _logger.LogInformation("{Message} {@LogEntry} {@Request}", message, logEntry, request);
        }

        /// <inheritdoc />
        public void LogWarning(string message, string correlationId, object request = null)
        {
            var logEntry = new LogEntry
            {
                CorrelationId = correlationId,
                InstrumentationDateTime = DateTime.UtcNow,
                Originator = typeof(T).FullName,
                MessageNumber = 1,
                Message = message
            };

            _logger.LogWarning("{Message} {@LogEntry} {@Request}", message, logEntry, request);
        }

        /// <inheritdoc />
        public void LogCritical(Exception exception, string correlationId, object request = null)
        {
            var logEntry = new LogEntry
            {
                CorrelationId = correlationId,
                InstrumentationDateTime = DateTime.UtcNow,
                Originator = typeof(T).FullName,
                MessageNumber = 1,
                Message = exception.Message
            };

            _logger.LogCritical("{Message} {@LogEntry} {@Request}", exception.Message, logEntry, request);
        }
    }
}
