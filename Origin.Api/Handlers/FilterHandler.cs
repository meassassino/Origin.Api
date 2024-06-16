using Origin.Api.Logging;
using Origin.Api.Services.Interfaces;

namespace Origin.Api.Handlers
{
    public class FilterHandler
    {
        public static IResult GetFilters(ILoggingService<FilterHandler> logger, IFilterService filterService, string name)
        {
            var response = filterService.GetFilterListFromFiles(name);
            if (response != null)
            {
                return Results.Content(response);
            }

            var notFoundMessage = $"could not get filters files for {name}";
            logger.LogError(notFoundMessage, new Guid().ToString());
            return TypedResults.NotFound(notFoundMessage);
        }
    }
}
