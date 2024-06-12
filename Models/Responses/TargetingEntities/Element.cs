using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.TargetingEntities
{
    public class Element
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("urn")]
        public string Urn { get; set; }

        [JsonPropertyName("facetUrn")]
        public string FacetUrn { get; set; }
    }
}
