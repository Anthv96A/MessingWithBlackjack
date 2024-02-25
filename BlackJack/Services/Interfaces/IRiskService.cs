using BlackJack.Models;

namespace BlackJack.Services.Interfaces;

public interface IRiskService
{
    bool ShouldRoll(Dealer player);
}
