using Origin.Api.Models.Requests;
using Origin.Api.Models.Responses.Insights;

namespace Origin.Api.Services.Interfaces
{
    public interface ISearchService
    {
        Task<List<InsightResponse>> SearchInsightsAsync(SearchAudienceRequest request);
    }
}
