using System.Text.Json.Serialization;

namespace Origin.Api.Models.Responses.Insights
{
    public class AudienceInsight
    {
        [JsonPropertyName("segmentations")]
        public List<Segmentation> Segmentations { get; set; }

        [JsonPropertyName("groupedBy")]
        public string GroupedBy { get; set; }
    }
}
