namespace Origin.Api.Common
{
    public static class Constants
    {
        public const string LocationsKey = "urn:li:adTargetingFacet:locations";

        public const string MatchPreferenceAnd = "AND";

        public const string MatchPreferenceOr = "OR";

        public const string ProtocolVersionHeaderKey = "x-restli-protocol-version";

        public const string ProtocolVersion = "2.0.0";

        public static Dictionary<string, string> FilterFilename = new()
        {
            {"degrees","degrees"},
            {"fieldsofstudy","fields_of_study"},
            {"generalinterests","general_interests"},
            {"industries","industries"},
            {"jobfunctions","job_functions"},
            {"locations","locations"},
            {"seniorities","seniorities"},
            {"skills","skills"},
            {"titles","titles"}
        };

        public static Dictionary<string, string> GroupNames = new()
        {
            { "urn:li:adTargetingFacet:productInterests", "Product Interests" },
            { "urn:li:adTargetingFacet:staffCountRanges", "Company Size" },
            { "urn:li:adTargetingFacet:bingCountry", "Country" }
        };
    }
}
