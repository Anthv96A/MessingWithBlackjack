using BlackJack.Enums;
using BlackJack.Models;
using BlackJack.Services;
using BlackJack.Services.Interfaces;
using Moq;

namespace BlackJack.UnitTests;

[TestFixture]
public class GameResultServiceTests
{
    private Mock<ICardService> _mockCardService;
    private GameResultService _gameResultService;

    [SetUp]
    public void Setup()
    {
        _mockCardService = new Mock<ICardService>();
        _gameResultService = new GameResultService(_mockCardService.Object);
    }

    [Test]
    public void GetResult_PlayerBust_ReturnsDealerWin()
    {
        var player = new Player();
        var dealer = new Dealer();

        _mockCardService.Setup(service => service.IsPlayerBust(player)).Returns(true);
        _mockCardService.Setup(service => service.IsPlayerBust(dealer)).Returns(false);

        var result = _gameResultService.GetResult(player, dealer);

        Assert.That(result, Is.EqualTo(GameResult.Dealer));
    }

    [Test]
    public void GetResult_DealerBust_ReturnsPlayerWin()
    {
        var player = new Player();
        var dealer = new Dealer();

        _mockCardService.Setup(service => service.IsPlayerBust(player)).Returns(false);
        _mockCardService.Setup(service => service.IsPlayerBust(dealer)).Returns(true);

        var result = _gameResultService.GetResult(player, dealer);

        Assert.That(result, Is.EqualTo(GameResult.Player));
    }


    [Test]
    public void GetResult_BothBust_Draw()
    {
        var player = new Player();
        var dealer = new Dealer();

        _mockCardService.Setup(service => service.IsPlayerBust(player)).Returns(true);
        _mockCardService.Setup(service => service.IsPlayerBust(dealer)).Returns(true);

        var result = _gameResultService.GetResult(player, dealer);

        Assert.That(result, Is.EqualTo(GameResult.Draw));
    }

    [Test]
    public void GetResult_SameScore_Draw()
    {
        var player = new Player();
        var dealer = new Dealer();

        _mockCardService.Setup(service => service.CalculateScoreTotalInHand(player)).Returns(20);
        _mockCardService.Setup(service => service.CalculateScoreTotalInHand(dealer)).Returns(20);

        var result = _gameResultService.GetResult(player, dealer);

        Assert.That(result, Is.EqualTo(GameResult.Draw));
    }

    [Test]
    public void GetResult_PlayerHigherScoreThanDealer_ReturnsPlayerWin()
    {
        var player = new Player();
        var dealer = new Dealer();

        _mockCardService.Setup(service => service.CalculateScoreTotalInHand(player)).Returns(19);
        _mockCardService.Setup(service => service.CalculateScoreTotalInHand(dealer)).Returns(18);

        var result = _gameResultService.GetResult(player, dealer);

        Assert.That(result, Is.EqualTo(GameResult.Player));
    }

    [Test]
    public void GetResult_DealerHigherScoreThanPlayer_ReturnsDealerWin()
    {
        var player = new Player();
        var dealer = new Dealer();

        _mockCardService.Setup(service => service.CalculateScoreTotalInHand(player)).Returns(17);
        _mockCardService.Setup(service => service.CalculateScoreTotalInHand(dealer)).Returns(18);

        var result = _gameResultService.GetResult(player, dealer);

        Assert.That(result, Is.EqualTo(GameResult.Dealer));
    }
}