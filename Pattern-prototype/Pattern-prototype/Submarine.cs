namespace Pattern_prototype
{
    /// <summary>
    /// Класс подводной лодки
    /// </summary>
    public class Submarine : WaterVehicle, IMyCloneable<Submarine>, ICloneable
    {
        public Submarine(string name, int speed) : base(name, speed, false) { }

        public Submarine(Submarine other) : base(other) { }

        public override Submarine Clone() => new Submarine(this);

        object ICloneable.Clone() => Clone();
    }
}
