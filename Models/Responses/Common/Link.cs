using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.Common
{
    public class Link
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("rel")]
        public string Rel { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }
    }
}
