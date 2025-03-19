namespace Pattern_prototype
{
    /// <summary>
    /// Класс для назесного транспорта
    /// </summary>
    public class LandVehicle : Vehicle, IMyCloneable<LandVehicle>, ICloneable
    {
        public int Wheels { get; set; }

        public LandVehicle(string name, int speed, int wheels) : base(name, speed)
        {
            Wheels = wheels;
        }

        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="other">Объект LandVehicle</param>
        public LandVehicle(LandVehicle other) : base(other)
        {
            Wheels = other.Wheels;
        }

        /// <summary>
        /// Метод для клонирования реализующий интерфейс IMyCloneable
        /// </summary>
        /// <returns>Склонированый объект LandVehicle</returns>
        public override LandVehicle Clone() => new LandVehicle(this);

        /// <summary>
        /// Метод для клонирования реализующий интерфейс ICloneable
        /// </summary>
        /// <returns>Склонированный объект LandVehicle</returns>
        object ICloneable.Clone() => Clone();

        public override void Move()
        {
            Console.WriteLine($"{Name} едет по земле со скростью {Speed} км/ч на {Wheels} колесах.");
        }
    }
}
