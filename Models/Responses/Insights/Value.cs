using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.Insights
{
    public class Value
    {
        [JsonPropertyName("audienceInsight")]
        public AudienceInsight AudienceInsight { get; set; }

        [JsonPropertyName("totalAudienceCount")]
        public int TotalAudienceCount { get; set; }

        //TODO: move to search result
        [JsonPropertyName("groupByName")]
        public string GroupByName { get; set; }
    }
}
