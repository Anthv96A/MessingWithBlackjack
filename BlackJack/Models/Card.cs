namespace BlackJack.Models;

public record Card
{
    public string Id => Guid.NewGuid().ToString();

    public required string Value { get; init; }

    public required string Suit { get; init; }

    public bool IsHonourCard()
    {
        return
            Value.Equals("jack", StringComparison.OrdinalIgnoreCase) ||
            Value.Equals("ace", StringComparison.OrdinalIgnoreCase) ||
            Value.Equals("queen", StringComparison.OrdinalIgnoreCase) ||
            Value.Equals("king", StringComparison.OrdinalIgnoreCase);
    }
}
