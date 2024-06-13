using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Origin.Api.Models.Requests;
using Origin.Api.Models.Responses.AdAccountsV2;
using Origin.Api.Models.Responses.Insights;
using Origin.Api.Models.Responses.TargetingEntities;
using Origin.Api.Services.Interfaces;
using Origin.Api.Settings;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static Origin.Api.Common.Constants;

namespace Origin.Api.Services
{
    public class LinkedInService : ILinkedInService
    {
        private readonly HttpClient _httpClient;
        private readonly LinkedInSettings _settings;

        public LinkedInService(HttpClient httpClient, IOptions<LinkedInSettings> settings)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _httpClient.BaseAddress = new Uri(_settings.ApiBaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.AccessToken); //TODO: use a middleware
        }

        #region Public_Services

        public async Task<InsightResponse> SearchInsightsAsync(InsightRequest request)
        {
            var queryParameters = new Dictionary<string, string>()
            {
                { "action", "audienceInsights" }, // TODO: make constant
            };
            var body = JsonSerializer.Serialize(request);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var apiPath = Path.Combine(_settings.ApiVersion, _settings.ApiAudienceInsightsPath);
            var requestUri = QueryHelpers.AddQueryString(apiPath, queryParameters);

            try
            {
                var response = await _httpClient.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var insightResponse = JsonSerializer.Deserialize<InsightResponse>(responseString);

                return insightResponse;
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request exceptions
                throw new Exception("Error occurred while making the API call", ex);
            }
            catch (JsonException ex)
            {
                // Handle JSON serialization/deserialization exceptions
                throw new Exception("Error occurred while processing the JSON response", ex);
            }
        }

        public async Task<TargetingEntitiesResponse> GetUrnsAsync(List<string> urnsList)
        {
            var escapedUrns = urnsList.Select(Uri.EscapeDataString).ToList();

            var urns = $"List({string.Join(",", escapedUrns)})";

            var apiPath = Path.Combine(_settings.ApiVersion, _settings.ApiAdTargetingEntitiesPath);
            var requestUri = $"{apiPath}?q=urns&urns={urns}&locale=(language:en,country:GB)"; // todo: make constant

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Add("Connection", "keep-Alive"); // TODO: make constant
            request.Headers.Add(ProtocolVersionHeaderKey, ProtocolVersion);
            request.Headers.Add("LinkedIn-Version", GetLatestVersionForLinkedInApiCalls());

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var urnsResponse = JsonSerializer.Deserialize<TargetingEntitiesResponse>(responseString);
            return urnsResponse;
        }

        public async Task<TargetingEntitiesResponse> GetFacetAsync(string name)
        {
            var apiPath = Path.Combine(_settings.ApiVersion, _settings.ApiAdTargetingEntitiesPath);
            var queryParameters = new Dictionary<string, string>
            {
                { "q","adTargetingFacet" }, //TODO: move to constants
                { "facet", $"urn:li:adTargetingFacet:{name}" },
                { "locale.language", "en" },
                { "locale.country", "US" },
                { "queryVersion", "QUERY_USES_URNS" }
            };

            var requestUri = QueryHelpers.AddQueryString(apiPath, queryParameters);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            request.Headers.Add("Connection", "keep-Alive"); // TODO: make constant
            request.Headers.Add("LinkedIn-Version", GetLatestVersionForLinkedInApiCalls());

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var facetsResponse = JsonSerializer.Deserialize<TargetingEntitiesResponse>(responseString);
            return facetsResponse;
        }

        public async Task<TargetingEntitiesResponse> GetTypeaheadFacetAsync(string parameter, string name, string entityType)
        {
            var apiPath = Path.Combine(_settings.ApiVersion, _settings.ApiAdTargetingEntitiesPath);
            var queryParameters = new Dictionary<string, string>
            {
                { "q","typeahead" }, //TODO: move to constants
                { "query" , parameter },
                { "facet", $"urn:li:adTargetingFacet:{name}" },
                { "entityType", entityType.ToUpperInvariant() },
                { "locale.language", "en" },
                { "locale.country", "US" },
                { "queryVersion", "QUERY_USES_URNS" },
                { "count", "50" }
            };

            var requestUri = QueryHelpers.AddQueryString(apiPath, queryParameters);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            request.Headers.Add("Connection", "keep-Alive"); // TODO: make constant
            request.Headers.Add("LinkedIn-Version", GetLatestVersionForLinkedInApiCalls());

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var facetsResponse = JsonSerializer.Deserialize<TargetingEntitiesResponse>(responseString);
            return facetsResponse;
        }

        public async Task<AdAccountsResponse> GetAdAccountsAsync()
        {
            var apiPath = Path.Combine(_settings.ApiVersion, _settings.AdAccountsPath);
            var queryParameters = new Dictionary<string, string>
            {
                { "q","search" }, //TODO: move to constants
            };

            var requestUri = QueryHelpers.AddQueryString(apiPath, queryParameters);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            request.Headers.Add(ProtocolVersionHeaderKey, ProtocolVersion);
            request.Headers.Add("Connection", "keep-Alive"); // TODO: make constant
            request.Headers.Add("LinkedIn-Version", GetLatestVersionForLinkedInApiCalls());

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var adAccountsResponse = JsonSerializer.Deserialize<AdAccountsResponse>(responseString);
            return adAccountsResponse;
        }

        private string GetLatestVersionForLinkedInApiCalls()
        {
            var currentDate = DateTime.Now;
            var previousMonthDate = currentDate.AddMonths(-1);
            var previousMonth = previousMonthDate.Month;
            var previousYear = previousMonthDate.Year;
            var version = previousYear.ToString("0000") + previousMonth.ToString("00");
            return version;
        }

        #endregion
    }
}
