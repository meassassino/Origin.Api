using Origin.Api.Models.Requests;
using Origin.Api.Models.Responses.AdAccountsV2;
using Origin.Api.Models.Responses.Insights;
using Origin.Api.Models.Responses.TargetingEntities;

namespace Origin.Api.Services.Interfaces
{
    public interface ILinkedInService
    {
        Task<InsightResponse> SearchInsightsAsync(InsightRequest request);

        Task<TargetingEntitiesResponse> GetUrnsAsync(List<string> urns);

        Task<TargetingEntitiesResponse> GetFacetAsync(string name);

        Task<TargetingEntitiesResponse> GetTypeaheadFacetAsync(string parameter, string name, string entityType);

        Task<AdAccountsResponse> GetAdAccountsAsync();
    }
}
