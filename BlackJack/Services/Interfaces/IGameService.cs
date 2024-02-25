using BlackJack.Enums;
using BlackJack.Models;

namespace BlackJack.Services.Interfaces;

public interface IGameService
{
    GameResult Play(Player player, Dealer dealer);
}
