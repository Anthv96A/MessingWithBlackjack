namespace BlackJack.Models;

public abstract record BasePlayer
{
    public string Id { get; } = Guid.NewGuid().ToString();

    public int GamesWon { get; private set; }

    private readonly IList<Card> _cardsInHand = new List<Card>();


    public void IncrementGamesWon() => GamesWon++;

    public void AddCardToHand(Card card) => _cardsInHand.Add(card);

    public IEnumerable<Card> GetCards() => _cardsInHand;

    public void EmptyHand() => _cardsInHand.Clear();
}
