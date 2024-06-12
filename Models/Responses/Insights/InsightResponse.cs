using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.Insights
{
    public class InsightResponse
    {
        [JsonPropertyName("value")]
        public Value Value { get; set; }
    }
}