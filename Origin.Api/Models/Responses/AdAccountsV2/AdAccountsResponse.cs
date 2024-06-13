using Origin.Api.Models.Responses.Common;
using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.AdAccountsV2
{
    public class AdAccountsResponse
    {
        [JsonPropertyName("paging")]
        public Paging Paging { get; set; }

        [JsonPropertyName("elements")]
        public List<Element> Elements { get; set; }
    }
}
