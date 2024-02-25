using BlackJack.Enums;
using BlackJack.Models;
using BlackJack.Services.Interfaces;

namespace BlackJack.Services;

internal class GameResultService(ICardService cardService) : IGameResultService
{
    public GameResult GetResult(Player player, Dealer dealer)
    {
        var isPlayerBust = cardService.IsPlayerBust(player);
        var isDealerBust = cardService.IsPlayerBust(dealer);

        if (isPlayerBust && isDealerBust)
        {
            return GameResult.Draw;
        }

        if (isPlayerBust)
        {
            return GameResult.Dealer;
        }

        if (isDealerBust)
        {
            return GameResult.Player;
        }

        var playerCardScore = cardService.CalculateScoreTotalInHand(player);
        var dealerCardScore = cardService.CalculateScoreTotalInHand(dealer);

        if (playerCardScore == dealerCardScore)
        {
            return GameResult.Draw;
        }

        return playerCardScore > dealerCardScore ? GameResult.Player : GameResult.Dealer;
    }
}
