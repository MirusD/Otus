namespace Pattern_prototype
{
    public static class CloneTest
    {
        public static void RunTests()
        {
            Console.WriteLine("=== Тестирование клонирования ===\n");

            TestClone(new Car("Легковой автомобиль", 120));
            TestClone(new Motorcycle("Мотоцикл", 90));
            TestClone(new Boat("Парусная лодка", 30));
            TestClone(new Submarine("Подводная лодка", 50));
        }

        private static void TestClone<T>(T original) where T : Vehicle, IMyCloneable<T>
        {
            T clone = (T)original.Clone();

            Console.WriteLine($"Оригинал: {GetObjectInfo(original)}");
            original.Move();

            Console.WriteLine($"Клон: {GetObjectInfo(clone)}");
            clone.Move();

            Console.WriteLine($"Равенство ссылок (должно быть false): {ReferenceEquals(original, clone)}");
            Console.WriteLine("=========");
        }

        private static string GetObjectInfo<T>(T obj) where T : Vehicle
        {
            return $"{obj.Name}, скорость: {obj.Speed}";
        }
    }
}
