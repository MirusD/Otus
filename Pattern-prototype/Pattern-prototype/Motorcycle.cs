namespace Pattern_prototype
{
    /// <summary>
    /// Класс мотоцикл
    /// </summary>
    public class Motorcycle : LandVehicle, IMyCloneable<Motorcycle>, ICloneable
    {
        public Motorcycle(string name, int speed) : base(name, speed, 2) { }

        public Motorcycle(Motorcycle other) : base(other) { }

        public override Motorcycle Clone() => new Motorcycle(this);

        object ICloneable.Clone() => Clone();
    }
}
