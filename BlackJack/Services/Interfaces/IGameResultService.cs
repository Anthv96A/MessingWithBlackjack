using BlackJack.Enums;
using BlackJack.Models;

namespace BlackJack.Services.Interfaces;

public interface IGameResultService
{
    GameResult GetResult(Player player, Dealer dealer);
}
