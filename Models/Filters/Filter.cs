using Origin.Api.Models.Filters.Common;
using System.Text.Json.Serialization;

namespace Origin.Api.Models.Filters
{
    public class Filter
    {
        [JsonPropertyName("elements")]
        public List<ElementBase> Elements { get; set; }
    }
}
