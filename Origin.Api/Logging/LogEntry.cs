using System.Text.Json.Serialization;

namespace Origin.Api.Logging
{
    /// <summary>
    /// Log entry class used for instrumentation
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Instrumentation date and time
        /// </summary>
        [JsonPropertyName("instrumentation_datetime_utc")]
        public DateTime InstrumentationDateTime { get; set; }

        /// <summary>
        /// Correlation id used for tracing
        /// </summary>
        [JsonPropertyName("correlation_id")]
        public string CorrelationId { get; set; }

        /// <summary>
        /// Originator
        /// </summary>
        [JsonPropertyName("originator")]
        public string Originator { get; set; }

        /// <summary>
        /// Message number
        /// </summary>
        [JsonPropertyName("message_number")]
        public int MessageNumber { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
