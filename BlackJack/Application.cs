using BlackJack.Enums;
using BlackJack.Models;
using BlackJack.Services.Interfaces;

namespace BlackJack;

internal class Application(IGameService gameService)
{
    public void Start()
    {
        var player = new Player();
        var dealer = new Dealer();

        StartGame(player, dealer);
    }

    private void StartGame(Player player, Dealer dealer)
    {
        while (IsPlaying())
        {
            var result = gameService.Play(player, dealer);
            Console.WriteLine();

            if (result == GameResult.Draw)
            {
                Console.WriteLine("This round was a draw");
            }
            else
            {
                Console.WriteLine($"Winner of the game is: {result}");

                switch (result)
                {
                    case GameResult.Player:
                        player.IncrementGamesWon();
                        break;

                    case GameResult.Dealer:
                        dealer.IncrementGamesWon();
                        break;
                }

                Console.WriteLine($"Games won by player: {player.GamesWon}");
                Console.WriteLine($"Games won by dealer: {dealer.GamesWon}");
            }

            Console.ReadKey();

            player.EmptyHand();
            dealer.EmptyHand();
            Console.Clear();
        }

        Console.WriteLine("Goodbye");
    }

    private static bool IsPlaying()
    {
        Console.WriteLine("Would you like to play? - Y/N");
        var answer = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(answer))
        {
            Console.WriteLine("Unknown answer");

            return IsPlaying();
        }

        if (answer.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (answer.Equals("n", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        Console.Clear();
        Console.WriteLine("Unknown answer");

        return IsPlaying();
    }
}
