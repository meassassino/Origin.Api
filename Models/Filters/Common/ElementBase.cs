using System.Text.Json.Serialization;

namespace Origin.Api.Models.Filters.Common
{
    public class ElementBase
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("$URN")]
        public string Urn { get; set; }

        [JsonPropertyName("name")]
        public Name Name { get; set; }

        [JsonPropertyName("alias")]
        public List<Alias> Alias { get; set; }

        [JsonPropertyName("rollup")]
        public string Rollup { get; set; }
    }
}
