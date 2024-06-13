using System.Text.Json.Serialization;

namespace Origin.Api.Models.Filters.Common
{
    public class Name
    {
        [JsonPropertyName("localized")]
        public Dictionary<string, string> Localized { get; set; }
    }
}
