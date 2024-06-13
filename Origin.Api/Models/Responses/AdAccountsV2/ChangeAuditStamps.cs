using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.AdAccountsV2
{
    public class ChangeAuditStamps
    {
        [JsonPropertyName("created")]
        public Created Created { get; set; }

        [JsonPropertyName("lastModified")]
        public LastModified LastModified { get; set; }
    }
}
