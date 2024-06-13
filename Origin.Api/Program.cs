using Origin.Api.Handlers;
using Origin.Api.Middleware;
using Origin.Api.Services;
using Origin.Api.Services.Interfaces;
using Origin.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddTransient<ISearchService, SearchService>();
builder.Services.AddTransient<ILinkedInService, LinkedInService>();
builder.Services.AddTransient<IFilterService, FilterService>();

// Settings
builder.Services.Configure<LinkedInSettings>(builder.Configuration.GetSection("LinkedInSettings"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS

var apiCorsPolicy = "ApiCorsPolicy";

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: apiCorsPolicy,
//        builder =>
//        {
//            builder.WithOrigins("http://localhost:3000", "https://localhost:3001")
//                .AllowAnyHeader()
//                .AllowAnyMethod()
//                .AllowCredentials();
//            //.WithMethods("OPTIONS", "GET");
//        });
//});


var app = builder.Build();

// Configure the HTTP request pipeline.

// Add the API Key Middleware
app.UseMiddleware<ApiKeyMiddleware>();

//* TODO: Move to different file  */
var swaggerSettings = builder.Configuration
    .GetSection(nameof(SwaggerSettings))
    .Get<SwaggerSettings>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Origin Api V1");
    if (!swaggerSettings.TryItOut)
    {
        c.SupportedSubmitMethods();
    }
});


app.UseHttpsRedirection();

// Use CORS
//app.UseCors(apiCorsPolicy);

//* TODO: Move to different file  */

app.MapPost("/audience/search", AudienceHandler.SearchAudienceAsync)
    .WithName("Audience")
    .WithOpenApi();

app.MapGet("/filter/{name}", FilterHandler.GetFilters)
    .WithName("Filter")
    .WithOpenApi();

app.MapGet("/facet/{name}", FacetHandler.GetFacets)
    .WithName("Facet")
    .WithOpenApi();

app.MapGet("/facet/typeahead/{parameter}/{name}/{entityType}", FacetHandler.GetTypeahead)
    .WithName("TypeaheadFacet")
    .WithOpenApi();

app.MapGet("/ad-accounts", AdAccountsHandler.GetAdAccounts)
    .WithName("AdAccounts")
    .WithOpenApi();

app.Run();