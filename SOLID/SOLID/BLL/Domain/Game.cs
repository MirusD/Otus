namespace SOLID.BLL.Domain
{
    class Game
    {
        public bool IsStarted { get; set; }

        public RangeNumbers Range { get; set; }

        public int Number { get; set; }

        public int Attempts { get; set; }

        public int AttemptsCount { get; set; } = 0;
    }
}
