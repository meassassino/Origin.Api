using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.Insights
{
    public class Segmentation
    {
        [JsonPropertyName("entityCount")]
        public int EntityCount { get; set; }

        [JsonPropertyName("entityPercentage")]
        public int EntityPercentage { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        // TODO: Move to search response

        [JsonPropertyName("resolvedUrn")]
        public string ResolvedUrn { get; set; }
    }
}
