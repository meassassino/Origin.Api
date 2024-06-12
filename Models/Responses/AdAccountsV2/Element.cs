using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.AdAccountsV2
{
    public class Element
    {
        [JsonPropertyName("test")]
        public bool Test { get; set; }

        [JsonPropertyName("notifiedOnCreativeRejection")]
        public bool NotifiedOnCreativeRejection { get; set; }

        [JsonPropertyName("notifiedOnEndOfCampaign")]
        public bool NotifiedOnEndOfCampaign { get; set; }

        [JsonPropertyName("servingStatuses")]
        public List<string> ServingStatuses { get; set; }

        [JsonPropertyName("notifiedOnCampaignOptimization")]
        public bool NotifiedOnCampaignOptimization { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("version")]
        public Version Version { get; set; }

        [JsonPropertyName("reference")]
        public string Reference { get; set; }

        [JsonPropertyName("notifiedOnCreativeApproval")]
        public bool NotifiedOnCreativeApproval { get; set; }

        [JsonPropertyName("changeAuditStamps")]
        public ChangeAuditStamps ChangeAuditStamps { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
