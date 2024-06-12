using Origin.Api.Services.Interfaces;

namespace Origin.Api.Handlers
{
    public class AdAccountsHandler
    {
        public static async Task<IResult> GetAdAccounts(ILinkedInService linkedInService)
        {
            var response = await linkedInService.GetAdAccountsAsync();
            if (response != null)
            {
                return TypedResults.Ok(response);
            }
            return TypedResults.NotFound("Facet not found");
        }
    }
}
