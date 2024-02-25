using BlackJack.Models;

namespace BlackJack.StartupTasks;

public interface IGameStartupTask
{
    IEnumerable<Card> Execute();
}

internal class GameStartupTask : IGameStartupTask
{
    private readonly string[] Suits = ["Spades", "Diamonds", "Clubs", "Hearts"];
    private const int MinCardNumber = 2;
    private const int MaxCardNumber = 10;

    public IEnumerable<Card> Execute()
    {
        List<Card> cards = [];

        foreach (var suit in Suits)
        {
            for (var cardNumber = MinCardNumber; cardNumber <= MaxCardNumber; cardNumber++)
            {
                cards.Add(new Card { Suit = suit, Value = cardNumber.ToString() });
            }

            cards.Add(new Card { Suit = suit, Value = "Ace" });
            cards.Add(new Card { Suit = suit, Value = "Jack" });
            cards.Add(new Card { Suit = suit, Value = "Queen" });
            cards.Add(new Card { Suit = suit, Value = "King" });
        }

        return cards.OrderBy(c => Guid.NewGuid());
    }
}
