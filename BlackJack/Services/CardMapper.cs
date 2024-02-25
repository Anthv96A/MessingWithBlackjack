using BlackJack.Models;
using BlackJack.Services.Interfaces;

namespace BlackJack.Services;

internal class CardMapper : ICardMapper
{
    public int MapCardToValue(Card card)
    {
        ArgumentNullException.ThrowIfNull(card);

        if (!card.IsHonourCard())
        {
            return int.Parse(card.Value);
        }

        return card.Value switch
        {
            "ace" => 1,
            _ => 10
        };
    }
}
