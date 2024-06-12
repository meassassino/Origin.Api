using Origin.Api.Services.Interfaces;
using static Origin.Api.Common.Constants;

namespace Origin.Api.Services
{
    public class FilterService: IFilterService
    {
        public string GetFilterListFromFiles(string filter)
        {
            if (!FilterFilename.ContainsKey(filter.ToLowerInvariant()))
            {
                return null;
            }

            try
            {
                var file = FilterFilename[filter];
                var pathToJson = Path.Combine(AppContext.BaseDirectory, "Content", "FilterData", $"{file}.json");
                var jsonFileContent = File.ReadAllText(pathToJson);
                return jsonFileContent;
            }
            catch (Exception ex)
            {
                // TODO: _logger.LogError(ex, "Error getting filters files for {filterFile}", file);
                // TODO: throw unknown exception
                throw;
            }
        }
    }
}
