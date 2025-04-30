
namespace SOLID.Domain.Interfaces
{
    interface IGameSettings
    {
        int MinValue { get; set; }
        int MaxValue { get; set; }
        int MaxAttemps { get; set; }
    }
}
