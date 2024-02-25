using BlackJack.Repositories;
using BlackJack.Services;
using BlackJack.Services.Interfaces;
using BlackJack.StartupTasks;
using Microsoft.Extensions.DependencyInjection;

namespace BlackJack;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            var serviceProvider = InitialiseServices();
            var application = serviceProvider.GetRequiredService<Application>();

            application.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error when running application", ex);
            throw;
        }
    }

    private static IServiceProvider InitialiseServices()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped<IGameStartupTask, GameStartupTask>();
        serviceCollection.AddSingleton<ICardRepository>(c =>
        {
            var cards = c.GetRequiredService<IGameStartupTask>().Execute().ToList();

            return new CardRepository(cards);
        });

        serviceCollection.AddScoped<Random>();
        serviceCollection.AddScoped<IRandomService, RandomService>();
        serviceCollection.AddScoped<ICardMapper, CardMapper>();
        serviceCollection.AddScoped<ICardService, CardService>();
        serviceCollection.AddScoped<IRiskService, RiskService>();
        serviceCollection.AddScoped<IGameResultService, GameResultService>();
        serviceCollection.AddScoped<IGameService, GameService>();
        serviceCollection.AddSingleton<Application>();

        return serviceCollection.BuildServiceProvider();
    }
}
