using System.Text.Json.Serialization;

namespace Origin.Api.Models.Requests
{
    public class SearchAudienceRequest
	{
        [JsonPropertyName("facetsToInclude")]
        public Dictionary<string, List<string>> FacetsToInclude { get; set; }

        [JsonPropertyName("facetsToExclude")]
        public Dictionary<string, List<string>> FacetsToExclude { get; set; }

        [JsonPropertyName("groupByList")]
        public List<string> GroupByList { get; set; }

        [JsonPropertyName("maxReturn")]
        public int MaxReturn { get; set; }

        [JsonPropertyName("matchPreference")]
        public string MatchPreference { get; set; }
    }
}
