using BlackJack.Exceptions;
using BlackJack.Models;
using BlackJack.Repositories;
using BlackJack.Services;
using BlackJack.Services.Interfaces;
using Moq;

namespace BlackJack.UnitTests;

[TestFixture]
public class CardServiceTests
{
    private Mock<ICardRepository> _mockCardRepository;
    private Mock<ICardMapper> _mockCardMapper;
    private CardService _cardService;

    [SetUp]
    public void Setup()
    {
        _mockCardRepository = new Mock<ICardRepository>();
        _mockCardMapper = new Mock<ICardMapper>();

        _cardService = new CardService(_mockCardRepository.Object, _mockCardMapper.Object);
    }

    [Test]
    public void GetCardFromDeck_ReturnsCard_RemovesCardFromDeck()
    {
        var expectedCard = new Card { Value = "1", Suit = "Jack" };
        _mockCardRepository.Setup(repo => repo.GetCard()).Returns(expectedCard);

        var card = _cardService.GetCardFromDeck();

        Assert.That(card, Is.EqualTo(expectedCard));
        _mockCardRepository.Verify(repo => repo.RemoveCard(card), Times.Once);
    }

    [Test]
    public void GetCardFromDeck_EmptyDeck_ThrowsEmptyDeckException()
    {
        _mockCardRepository.Setup(repo => repo.GetCard()).Returns(null as Card);

        Assert.Throws<EmptyDeckException>(() => _cardService.GetCardFromDeck());
    }

    [Test]
    public void CalculateScoreTotalInHand_WithCards_CalculatesScore()
    {
        var player = new Player();
        player.AddCardToHand(new Card { Value = "ace", Suit = "Jack" });
        player.AddCardToHand(new Card { Value = "king", Suit = "Jack" });

        _mockCardMapper.Setup(mapper => mapper.MapCardToValue(It.IsAny<Card>())).Returns<Card>(card => card.Value == "ace" ? 1 : 10);

        var totalScore = _cardService.CalculateScoreTotalInHand(player);

        Assert.That(totalScore, Is.EqualTo(11));
    }

    [Test]
    public void IsPlayerBust_ScoreOver21_ReturnsTrue()
    {
        var player = new Player();
        player.AddCardToHand(new Card { Value = "10", Suit = "jack" });
        player.AddCardToHand(new Card { Value = "10", Suit = "jack" });
        player.AddCardToHand(new Card { Value = "10", Suit = "jack" });

        _mockCardMapper.Setup(mapper => mapper.MapCardToValue(It.IsAny<Card>())).Returns(10);

        var isBust = _cardService.IsPlayerBust(player);

        Assert.That(isBust, Is.True);
    }

    [Test]
    public void IsPlayerBust_ScoreUnder21_ReturnsFalse()
    {
        var player = new Player();
        player.AddCardToHand(new Card { Value = "10", Suit = "jack" });

        _mockCardMapper.Setup(mapper => mapper.MapCardToValue(It.IsAny<Card>())).Returns(10);

        var isBust = _cardService.IsPlayerBust(player);

        Assert.That(isBust, Is.False);
    }
}
