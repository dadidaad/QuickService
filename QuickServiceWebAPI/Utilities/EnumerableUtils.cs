namespace QuickServiceWebAPI.Utilities
{
    public static class EnumerableUtils
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }
    }
}
