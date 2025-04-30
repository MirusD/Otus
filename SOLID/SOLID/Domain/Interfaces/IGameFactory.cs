namespace SOLID.Domain.Interfaces
{
    interface IGameFactory
    {
        IGame CreateGame(IGameSettings gameSettings);
    }
}
