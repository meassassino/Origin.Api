using Origin.Api.Logging;
using Origin.Api.Services.Interfaces;
using System.Text.RegularExpressions;

namespace Origin.Api.Handlers
{
    public class FilterHandler
    {
        public static IResult GetFilters(ILoggingService<FilterHandler> logger, IFilterService filterService, string name)
        {
            // TODO: use MediatR
            if (!IsValidFilterName(name))
            {
                return BadRequestResponse(logger, name);
            }

            var response = filterService.GetFilterListFromFiles(name);
            return response != null ? Results.Content(response) : NotFoundResponse(logger, name);
        }

        // TODO: use middleware / events pattern
        private static IResult NotFoundResponse(ILoggingService<FilterHandler> logger, string name)
        {
            var notFoundMessage = $"could not get filters files for {name}";
            logger.LogError(notFoundMessage, new Guid().ToString());
            return TypedResults.NotFound(notFoundMessage);
        }

        // TODO: use middleware / events pattern
        private static IResult BadRequestResponse(ILoggingService<FilterHandler> logger, string name)
        {
            var notFoundMessage = $"invalid filter name {name}";
            logger.LogError(notFoundMessage, new Guid().ToString());
            return TypedResults.BadRequest(notFoundMessage);
        }

        // TODO: mediatr handler
        private static bool IsValidFilterName(string name)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            return !string.IsNullOrWhiteSpace(name)
                   && regexItem.IsMatch(name);
        }
    }
}
