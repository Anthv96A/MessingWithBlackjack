using BlackJack.Exceptions;
using BlackJack.Models;
using BlackJack.Repositories;
using BlackJack.Services.Interfaces;

namespace BlackJack.Services;

internal class CardService(
    ICardRepository cardRepository,
    ICardMapper cardMapper) : ICardService
{
    public const int MaxAllowedScore = 21;

    public Card GetCardFromDeck()
    {
        var card = cardRepository.GetCard();

        if (card is null)
        {
            throw new EmptyDeckException();
        }

        cardRepository.RemoveCard(card);

        return card;
    }

    public int CalculateScoreTotalInHand(BasePlayer player)
    {
        return CalculateScore(player);
    }

    public bool IsPlayerBust(BasePlayer player)
    {
        var score = CalculateScore(player);

        return score > MaxAllowedScore;
    }

    private int CalculateScore(BasePlayer player)
    {
        var cardsInHand = player.GetCards();

        if (!cardsInHand.Any())
        {
            return 0;
        }

        return cardsInHand.Sum(cardMapper.MapCardToValue);
    }
}
