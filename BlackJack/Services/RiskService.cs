using BlackJack.Models;
using BlackJack.Services.Interfaces;

namespace BlackJack.Services;

internal class RiskService(
    IRandomService randomService,
    ICardService cardService) : IRiskService
{
    private const int HalfMaxScore = 10;
    private const int HighRiskCardScore = 17;

    public bool ShouldRoll(Dealer player)
    {
        var totalScore = cardService.CalculateScoreTotalInHand(player);

        if (totalScore == CardService.MaxAllowedScore)
        {
            return false;
        }

        if (totalScore <= HalfMaxScore)
        {
            return true;
        }

        var ramdomValue = randomService.Next();

        var rollChance = totalScore >= HighRiskCardScore ? 2 : 5;

        return ramdomValue > rollChance;
    }
}