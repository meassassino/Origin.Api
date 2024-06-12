using Origin.Api.Models.Requests;
using Origin.Api.Models.Responses.Insights;
using Origin.Api.Services.Interfaces;
using Origin.Api.Utilities;
using static Origin.Api.Common.Constants;

namespace Origin.Api.Services
{
    // TODO: SearchService
    public class SearchService : ISearchService
    {
        private const string AdAccount = "urn:li:sponsoredAccount:511357593";

        private readonly ILinkedInService _linkedInService;

        public SearchService(ILinkedInService linkedInService)
        {
            _linkedInService = linkedInService;
        }
        public async Task<List<InsightResponse>> SearchInsightsAsync(SearchAudienceRequest request)
        {
            // TODO: use middleware to cache this token
            //if (!string.IsNullOrWhiteSpace(accessToken))
            //{
            //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //}

            var tasks = request.GroupByList.Select(groupBy => GetInsightsAsync(request, groupBy)).ToList();
            var insights = await Task.WhenAll(tasks);

            var result = insights.ToList();
            await ResolveUrnsAsync(result);
            return result;
        }

        private async Task<InsightResponse> GetInsightsAsync(SearchAudienceRequest request, string groupBy)
        {
            var facetsToInclude = GetFacetsToInclude(request.MatchPreference, request);

            var insightRequest = new InsightRequest
            {
                Request = new RequestDto
                {
                    MaxReturnCount = request.MaxReturn,
                    RequestMetaData = new RequestMetaData
                    {
                        SponsoredAccount = AdAccount
                    },
                    TargetingCriteria = new TargetingCriteria
                    {
                        Include = facetsToInclude
                    },
                    GroupBy = groupBy
                }
            };

            return await _linkedInService.SearchInsightsAsync(insightRequest);
        }

        /*///////////////////////////////////////////////////////////*/

        // TODO: Move to Urn Service?
        private async Task ResolveUrnsAsync(List<InsightResponse> insightResponse)
        {
            // Todo: split the method

            var segmentValues = insightResponse.Where(x =>
                    x.Value.AudienceInsight.Segmentations != null && x.Value.AudienceInsight.Segmentations.Any())
                .SelectMany(x => x.Value.AudienceInsight.Segmentations.Select(s => s.Value)
                    .Concat(new[] { x.Value.AudienceInsight.GroupedBy })).ToList();

            if (segmentValues.IsAny())
            {
                var urnsResponse = await _linkedInService.GetUrnsAsync(segmentValues);

                var urnDict = urnsResponse.Elements
                    .GroupBy(x => x.Urn)
                    .ToDictionary(g => g.Key.HandleKeys(), g => g.ToList());

                insightResponse.ForEach(insight =>
                {
                    try
                    {
                        insight.Value.GroupByName = urnDict[insight.Value.AudienceInsight.GroupedBy].First().Name;
                    }
                    catch (KeyNotFoundException)
                    {
                        insight.Value.GroupByName = GroupNames[insight.Value.AudienceInsight.GroupedBy];
                    }

                    insight.Value.AudienceInsight.Segmentations.ForEach(segmentation =>
                    {
                        try
                        {
                            segmentation.ResolvedUrn = urnDict[segmentation.Value].First().Name;
                        }
                        catch (KeyNotFoundException)
                        {
                            segmentation.ResolvedUrn = GroupNames[segmentation.Value];
                        }
                    });
                });
            }

        }

        /*///////////////////////////////////////////////////////////*/


        // TODO:Move to Facet Service?

        /*///////////////////////////////////////////////////////////*/

        private Include GetFacetsToInclude(string matchPreference, SearchAudienceRequest request)
        {
            return matchPreference.ToUpperInvariant() switch
            {
                MatchPreferenceAnd => GetMatchPreferenceAnd(request),
                MatchPreferenceOr => GetMatchPreferenceOr(request),
                _ => new Include()
            };
        }

        private Include GetMatchPreferenceAnd(SearchAudienceRequest request)
        {
            var andList = request.FacetsToInclude.Select(x => new And
            {
                Or = new Dictionary<string, List<string>> { { x.Key, x.Value } }
            }).ToList();

            return new Include { And = andList };
        }

        private Include GetMatchPreferenceOr(SearchAudienceRequest request)
        {
            var locations = request.FacetsToInclude.Where(x => x.Key.Equals(LocationsKey)).ToDictionary();
            var facets = request.FacetsToInclude.Where(x => !x.Key.Equals(LocationsKey)).ToDictionary();

            return new Include
            {
                And = new List<And> { new() { Or = facets }, new() { Or = locations } }
            };
        }

        /*///////////////////////////////////////////////////////////*/

    }
}
