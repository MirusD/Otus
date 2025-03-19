namespace Pattern_prototype
{
    /// <summary>
    /// Базовый класс
    /// </summary>
    public class Vehicle : IMyCloneable<Vehicle>, ICloneable
    {
        public string Name { get; set; }
        public int Speed { get; set; }

        public Vehicle(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }

        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="other">Объект Vehicle</param>
        public Vehicle(Vehicle other) : this(other.Name, other.Speed) { }

        /// <summary>
        /// Метод для клонирования реализующий интерфей IMyCloneable
        /// </summary>
        /// <returns>Склонированный объект Vehicle</returns>
        public virtual Vehicle Clone() => new Vehicle(this);

        /// <summary>
        /// Методо для клонирования реализующий интерфейс ICloneable
        /// </summary>
        /// <returns>Склонированный объект Vehicle </returns>
        object ICloneable.Clone() => Clone();

        public virtual void Move()
        {
            Console.WriteLine($"{Name} движется со скоростью {Speed} км/ч");
        }

    }
}
