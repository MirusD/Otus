using System;
using System.Diagnostics;

namespace Reflection
{
    /// <summary>
    /// Клас для измерения времени выполнения различны операций.
    /// </summary>
    public static class BenchmarkTime
    {
        /// <summary>
        /// Измеряет время выполнения функции.
        /// </summary>
        /// <typeparam name="T">Тип результата, возвращаемого функцией.</typeparam>
        /// <param name="func">Функция, выполнение которой нужно замерить.</param>
        /// <returns>Кортеж, содержащий результат выполнения функции и время, затраченное на ее выполнение.</returns>
        public static (T result, Stopwatch time) Measure<T>(Func<T> func)
        {
            var sw = Stopwatch.StartNew();
            T result = func();
            sw.Stop();

            return (result, sw);
        }
    }
}
