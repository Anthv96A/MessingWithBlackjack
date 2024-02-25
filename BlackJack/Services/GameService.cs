using BlackJack.Enums;
using BlackJack.Models;
using BlackJack.Services.Interfaces;

namespace BlackJack.Services;

internal class GameService(
    ICardService cardService,
    IGameResultService gameResultService,
    IRiskService riskService) : IGameService
{
    public GameResult Play(Player player, Dealer dealer)
    {
        InitialisePlayersHand(player, dealer);

        while (PlayerIsPlaying(player))
        {
            DrawCardToHand(player);
        }

        while (DealerIsPlaying(dealer))
        {
            DrawCardToHand(dealer);
        }

        return gameResultService.GetResult(player, dealer);
    }

    private bool PlayerIsPlaying(Player player)
    {
        if (cardService.IsPlayerBust(player))
        {
            Console.WriteLine($"Player is bust!");
            return false;
        }

        var playerTotalScore = cardService.CalculateScoreTotalInHand(player);
        Console.WriteLine($"Player score {playerTotalScore}");
        Console.WriteLine("Type 'T' to twist or 'S' to stick");

        var option = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(option))
        {
            Console.WriteLine("Not a valid option");
            return PlayerIsPlaying(player);
        }

        if (option.Equals("S", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (!option.Equals("T", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Not a valid option");
            return PlayerIsPlaying(player);
        }

        return true;
    }


    private bool DealerIsPlaying(Dealer dealer)
    {
        if (cardService.IsPlayerBust(dealer))
        {
            Console.WriteLine($"Dealer is bust!");
            return false;
        }

        var dealerTotalScore = cardService.CalculateScoreTotalInHand(dealer);
        Console.WriteLine($"Dealer score {dealerTotalScore}");

        if (!riskService.ShouldRoll(dealer))
        {
            Console.WriteLine($"Dealer is sticking");
            return false;
        }

        Console.WriteLine($"Dealer is twisting");
        Task.Delay(1000).Wait();

        return true;
    }

    private void DrawCardToHand(BasePlayer player)
    {
        player.AddCardToHand(cardService.GetCardFromDeck());
    }

    public void InitialisePlayersHand(Player player, Dealer dealer)
    {
        for (var i = 0; i < 1; i++)
        {
            DrawCardToHand(player);

            DrawCardToHand(dealer);
        }
    }
}
