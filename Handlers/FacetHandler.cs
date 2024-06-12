using Origin.Api.Services.Interfaces;

namespace Origin.Api.Handlers
{
    public static class FacetHandler
    {
        public static async Task<IResult> GetFacets(ILinkedInService linkedInService, string name)
        {
            var response = await linkedInService.GetFacetAsync(name);
            if (response != null)
            {
                return TypedResults.Ok(response);
            }
            return TypedResults.NotFound("Facet not found");
        }

        public static async Task<IResult> GetTypeahead(ILinkedInService linkedInService, string parameter, string name, string entityType)
        {
            var response = await linkedInService.GetTypeaheadFacetAsync(parameter,name, entityType);
            if (response != null)
            {
                return TypedResults.Ok(response);
            }
            return TypedResults.NotFound("Facet not found");
        }
    }
}
