using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.AdAccountsV2
{
    public class Created
    {
        [JsonPropertyName("time")]
        public long Time { get; set; }
    }
}
