using BlackJack.Models;

namespace BlackJack.Repositories;

public interface ICardRepository
{
    Card? GetCard();

    void RemoveCard(Card card);
}
