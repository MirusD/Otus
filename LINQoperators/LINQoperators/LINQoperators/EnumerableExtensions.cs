namespace LINQoperators
{
    static class EnumerableExtensions
    {
        public static IEnumerable<TSource> Top<TSource>(this IEnumerable<TSource> source, double percent)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (percent <= 0 || percent > 100)
                throw new ArgumentException($"Неверное значение аргумента percent = {percent}. " +
                    $"Аргумент percent должен иметь значение от 0 до 100");

            int n = (int)Math.Ceiling(source.Count() * (percent / 100));
            var sortedList = source.OrderByDescending(element => element).ToList();

            return sortedList.Take(n);
        }

        public static IEnumerable<TSource> Top<TSource, TKey>(this IEnumerable<TSource> source, double percent, Func<TSource, TKey> keySelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (percent <= 0 || percent > 100)
                throw new ArgumentException($"Неверное значение аргумента percent = {percent}. " +
                    $"Аргумент percent должен иметь значение от 0 до 100");

            int n = (int)Math.Ceiling(source.Count() * (percent / 100));
            var sortedList = source.OrderByDescending(keySelector).ToList();

            return sortedList.Take(n);
        }
    }
}
