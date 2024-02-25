
using BlackJack.Models;
using BlackJack.Services;

namespace BlackJack.UnitTests;

[TestFixture]
public class CardMapperTests
{
    private CardMapper _cardMapper;

    [SetUp]
    public void SetUp()
    {
        _cardMapper = new();
    }

    [TestCase("2", ExpectedResult = 2)]
    [TestCase("3", ExpectedResult = 3)]
    [TestCase("4", ExpectedResult = 4)]
    [TestCase("5", ExpectedResult = 5)]
    [TestCase("6", ExpectedResult = 6)]
    [TestCase("7", ExpectedResult = 7)]
    [TestCase("8", ExpectedResult = 8)]
    [TestCase("9", ExpectedResult = 9)]
    [TestCase("10", ExpectedResult = 10)]
    public int MapCardToValue_WithNonHonourCard_ReturnsNumericValue(string cardValue)
    {
        var card = new Card { Value = cardValue, Suit = "" };
        return _cardMapper.MapCardToValue(card);
    }

    [TestCase("ace", ExpectedResult = 1)]
    [TestCase("king", ExpectedResult = 10)]
    [TestCase("queen", ExpectedResult = 10)]
    [TestCase("jack", ExpectedResult = 10)]
    public int MapCardToValue_WithHonourCard_ReturnsExpectedValue(string cardValue)
    {
        var card = new Card { Value = cardValue, Suit = "" };
        return _cardMapper.MapCardToValue(card);
    }

    [Test]
    public void MapCardToValue_WithNullCard_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _cardMapper.MapCardToValue(null));
    }
}
