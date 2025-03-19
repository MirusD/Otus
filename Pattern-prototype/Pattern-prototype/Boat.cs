namespace Pattern_prototype
{
    /// <summary>
    /// Класс лодки
    /// </summary>
    public class Boat : WaterVehicle, IMyCloneable<Boat>, ICloneable
    {
        public Boat(string name, int speed) : base(name, speed, true) { }

        public Boat(Boat other) : base(other) { }

        public override Boat Clone() => new Boat(this);

        object ICloneable.Clone() => Clone();
    }
}
