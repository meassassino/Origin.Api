using Microsoft.AspNetCore.Mvc;
using Origin.Api.Models.Requests;
using Origin.Api.Services.Interfaces;

namespace Origin.Api.Handlers
{
    public static class AudienceHandler
    {
        public static async Task<IResult> SearchAudienceAsync(ISearchService searchService,
             [FromBody] SearchAudienceRequest request)
        {
            var response = await searchService.SearchInsightsAsync(request);

            return Results.Json(response);
        }
    }
}
