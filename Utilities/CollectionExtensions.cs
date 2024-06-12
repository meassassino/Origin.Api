namespace Origin.Api.Utilities
{
    public static class CollectionExtensions
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }
    }
}
