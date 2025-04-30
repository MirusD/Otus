using System;
namespace SOLID.Domain.Interfaces
{
    interface INumberGenerator
    {
        public int Generate(int minValue, int maxValue);
    }
}
