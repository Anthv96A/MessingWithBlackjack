using BlackJack.Models;

namespace BlackJack.Services.Interfaces;

public interface ICardService
{
    Card GetCardFromDeck();

    int CalculateScoreTotalInHand(BasePlayer player);

    bool IsPlayerBust(BasePlayer player);
}
