using Origin.Api.Services.Interfaces;

namespace Origin.Api.Handlers
{
    public static class FilterHandler
    {
        public static IResult GetFilters(IFilterService filterService, string name)
        {
            var response = filterService.GetFilterListFromFiles(name);
            if (response != null)
            {
                return Results.Content(response);
            }
            return TypedResults.NotFound("Filter not found");
        }
    }
}
