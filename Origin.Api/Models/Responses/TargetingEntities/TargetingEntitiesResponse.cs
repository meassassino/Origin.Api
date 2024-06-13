using Origin.Api.Models.Responses.Common;
using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.TargetingEntities
{
    public class TargetingEntitiesResponse
    {
        [JsonPropertyName("paging")]
        public Paging Paging { get; set; }

        [JsonPropertyName("elements")]
        public List<Element> Elements { get; set; }
    }
}
