namespace Origin.Api.Utilities
{
    public static class StringExtensions
    {
        public static string HandleKeys(this string input)
        {
            return input.Replace("organization", "company");
        }
    }
}
