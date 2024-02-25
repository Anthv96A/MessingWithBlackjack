using BlackJack.Services.Interfaces;

namespace BlackJack.Services;

internal class RandomService(Random random) : IRandomService
{
    public int Next() => random.Next(1, 10);
}