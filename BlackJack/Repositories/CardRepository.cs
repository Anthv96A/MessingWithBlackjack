using BlackJack.Models;

namespace BlackJack.Repositories;

internal class CardRepository(ICollection<Card> cards) : ICardRepository
{
    public Card? GetCard()
    {
        return cards?.FirstOrDefault();
    }

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
    }
}
