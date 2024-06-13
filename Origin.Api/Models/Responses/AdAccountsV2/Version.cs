using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.AdAccountsV2
{
    public class Version
    {
        [JsonPropertyName("versionTag")]
        public string VersionTag { get; set; }
    }
}
