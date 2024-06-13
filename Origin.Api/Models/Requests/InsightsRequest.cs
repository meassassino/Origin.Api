using System.Text.Json.Serialization;

namespace Origin.Api.Models.Requests
{
    public class InsightRequest
    {
        [JsonPropertyName("request")]
        public RequestDto Request { get; set; }
    }

    public class RequestDto
    {
        [JsonPropertyName("maxReturnCount")]
        public int MaxReturnCount { get; set; }

        [JsonPropertyName("requestMetaData")]
        public RequestMetaData RequestMetaData { get; set; }

        [JsonPropertyName("targetingCriteria")]
        public TargetingCriteria TargetingCriteria { get; set; }

        [JsonPropertyName("groupBy")]
        public string GroupBy { get; set; }
    }

    public class RequestMetaData
    {
        [JsonPropertyName("sponsoredAccount")]
        public string SponsoredAccount { get; set; }
    }

    public class TargetingCriteria
    {
        [JsonPropertyName("include")]
        public Include Include { get; set; }

        [JsonPropertyName("exclude")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Exclude Exclude { get; set; }
    }

    public class Include
    {
        [JsonPropertyName("and")]
        public List<And> And { get; set; }
    }

    public class Exclude
    {
        [JsonPropertyName("and")]
        public List<And> And { get; set; }
    }

    public class And
    {
        [JsonPropertyName("or")]
        public Dictionary<string, List<string>> Or { get; set; }
    }
}
