namespace Pattern_prototype
{
    /// <summary>
    /// Класс автомобиля
    /// </summary>
    public class Car : LandVehicle, IMyCloneable<Car>, ICloneable
    {
        public Car(string name, int speed) : base(name, speed, 4) { }

        public Car(Car other) : base(other) { }

        public override Car Clone() => new Car(this);

        object ICloneable.Clone() => Clone();
    }
}
