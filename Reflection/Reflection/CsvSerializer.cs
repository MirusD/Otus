using System.Reflection;

namespace Reflection
{
    /// <summary>
    /// Класс, предоставляющии методы для сериализации и десериализации объектов в формате CSV.
    /// </summary>
    /// <typeparam name="T">Тип объекта, который будет сериализован или десериализован.</typeparam>
    public static class CsvSerializer<T> where T : new()
    {
        /// <summary>
        /// Сириализует объект типа <typeparamref name="T"/> в строку CSV.
        /// Каждый публичный свойство объекта преобразуется в строку и разделяется запятыми.
        /// </summary>
        /// <param name="obj">Объект, который нужно сериализовать.</param>
        /// <returns>Строка CSV, содержащая значения свойств объекта, разделенные запятыми.</returns>
        public static string Serialize(T obj)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            var values = properties.Select(p => p.GetValue(obj)?.ToString() ?? "").ToArray();
            return string.Join(",", values);
        }

        /// <summary>
        /// Десериализует строку CSV в объект типа <typeparamref name="T"/>.
        /// Строка CSV должна содержать значения, разделенные запятыми, которые будут преобразованы в свойства объекта.
        /// </summary>
        /// <param name="csv">Строка CSV, которая будет десериализована в объект.</param>
        /// <returns>Объект типа <typeparamref name="T"/> с значениями, полученными из строки CSV.</returns>
        /// <exception cref="InvalidOperationException">Выбрасывается, если количество полей в CSV не соответствует количеству свойств объекта.</exception>
        public static T Deserialize(string csv)
        {
            var values = csv.Split(',');

            var obj = new T();
            var propetries = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (values.Length != propetries.Length)
            {
                throw new InvalidOperationException("Количество полей CSV не соответствует свойствам объекта.");
            }

            for (int i = 0; i < propetries.Length; i++)
            {
                var prop = propetries[i];
                var type = prop.PropertyType;
                var value = Convert.ChangeType(values[i], type);
                prop.SetValue(obj, value);
            }

            return obj;
        }
    }
}
