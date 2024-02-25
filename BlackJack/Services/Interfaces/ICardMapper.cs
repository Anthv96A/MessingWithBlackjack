using BlackJack.Models;

namespace BlackJack.Services.Interfaces;

public interface ICardMapper
{
    int MapCardToValue(Card card);
}
