namespace Pattern_prototype
{
    /// <summary>
    /// Класс для водного транспорта
    /// </summary>
    public class WaterVehicle : Vehicle, IMyCloneable<WaterVehicle>, ICloneable
    {
        public bool HasSails { get; set; }

        public WaterVehicle(string name, int speed, bool hasSails) : base(name, speed)
        {
            HasSails = hasSails;
        }

        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="other">Объект WaterVehicle</param>
        public WaterVehicle(WaterVehicle other) : base(other)
        {
            HasSails = other.HasSails;
        }

        /// <summary>
        /// Метод для копирования
        /// </summary>
        /// <returns>скопированый объект WaterVehicle</returns>
        public override WaterVehicle Clone() => new WaterVehicle(this);

        /// <summary>
        /// Метод для клонирования реализующий интерфейс ICloneable
        /// </summary>
        /// <returns>Склонированный объект WaterVehicle</returns>
        object ICloneable.Clone() => Clone();

        public override void Move()
        {
            Console.WriteLine($"{Name} плывет по воде со скоростью {Speed} км/ч. Паруса: {(HasSails ? "есть" : "Нет")}.");
        }
    }
}
