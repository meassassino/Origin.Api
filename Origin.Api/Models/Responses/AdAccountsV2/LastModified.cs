using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.AdAccountsV2
{
    public class LastModified
    {
        [JsonPropertyName("time")]
        public long Time { get; set; }
    }
}
