using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.Common
{
    public class Paging
    {
        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("links")]
        public List<Link> Links { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
