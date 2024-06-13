namespace Origin.Api.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "x-api-key";
    private readonly IConfiguration _configuration;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("API Key was not provided.");
            return;
        }

        var apiKey = _configuration.GetValue<string>("ApiKey");

        if (!apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Unauthorized client.");
            return;
        }

        AddOriginHeaders(context);
        await _next(context);
    }

    private void AddOriginHeaders(HttpContext context)
    {

    }
}