using Origin.Api.Logging;
using Origin.Api.Services.Interfaces;
using System.Text.RegularExpressions;

namespace Origin.Api.Handlers
{
    /// <summary>
    /// Handles filtering operations.
    /// </summary>
    public class FilterHandler
    {
        /// <summary>
        /// Gets filters based on the provided name.
        /// </summary>
        /// <param name="logger">Logging service.</param>
        /// <param name="filterService">Filter service.</param>
        /// <param name="name">Name of the filter.</param>
        /// <returns>Result of the filter operation.</returns>
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
        /// <summary>
        /// Generates a not found response.
        /// </summary>
        /// <param name="logger">Logging service.</param>
        /// <param name="name">Name of the filter.</param>
        /// <returns>Not found response.</returns>
        private static IResult NotFoundResponse(ILoggingService<FilterHandler> logger, string name)
        {
            var notFoundMessage = $"could not get filters files for {name}";
            logger.LogError(notFoundMessage, new Guid().ToString());
            return TypedResults.NotFound(notFoundMessage);
        }

        // TODO: use middleware / events pattern
        /// <summary>
        /// Generates a bad request response.
        /// </summary>
        /// <param name="logger">Logging service.</param>
        /// <param name="name">Name of the filter.</param>
        /// <returns>Bad request response.</returns>
        private static IResult BadRequestResponse(ILoggingService<FilterHandler> logger, string name)
        {
            var notFoundMessage = $"invalid filter name {name}";
            logger.LogError(notFoundMessage, new Guid().ToString());
            return TypedResults.BadRequest(notFoundMessage);
        }

        // TODO: mediatr handler
        /// <summary>
        /// Validates the filter name.
        /// </summary>
        /// <param name="name">Name of the filter.</param>
        /// <returns>True if the filter name is valid, otherwise false.</returns>
        private static bool IsValidFilterName(string name)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            return !string.IsNullOrWhiteSpace(name)
                   && regexItem.IsMatch(name);
        }
    }
}
