using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WillcoxGame.GamePlay;
using WillcoxGame.Interfaces;

// Dependency Injection binding
var services = new ServiceCollection();
services.AddTransient<IGameService, GameService>();
services.AddLogging((builder) =>
{
    builder.SetMinimumLevel(LogLevel.Debug);
    builder.AddConsole();
});
var serviceProvider = services.BuildServiceProvider();

// Create game service & play it
var gameService = serviceProvider.GetRequiredService<IGameService>();
gameService.InitialiseGame(6, 8);
#pragma warning disable S1481 // Unused local variables should be removed
var winner = gameService.PlayGame();
#pragma warning restore S1481 // Unused local variables should be removed

Console.WriteLine("Press a key to quit");
Console.ReadKey();