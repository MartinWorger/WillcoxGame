using Microsoft.Extensions.Logging;
using WillcoxGame.Interfaces;
using WillcoxGame.Models;

namespace WillcoxGame.GamePlay
{
    internal class GameService : IGameService
    {
        internal const int MinimumPlayers = 2;
        internal const int MinimumWords = 2;

        private readonly ILogger<GameService> logger;
        private List<Player>? players;
        private int numberOfWords = 0;

        public GameService(ILogger<GameService> logger)
        {
            this.logger = logger;
        }

        public void InitialiseGame(int numberOfPlayers, int numberOfWords)
        {
            if (numberOfPlayers < MinimumPlayers)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfPlayers));
            }
            if (numberOfWords < MinimumWords)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfWords));
            }

            InitialisePlayers(numberOfPlayers);
            this.numberOfWords = numberOfWords;

            logger.LogInformation("Initialised game with {numberOfPlayers} players and {numberOfWords} words", numberOfPlayers, numberOfWords);
        }

        public int PlayGame()
        {
            if (players?.Count < MinimumPlayers || numberOfWords < MinimumWords)
            {
                throw new InvalidOperationException("Game not initialised");
            }

            //Start index at the last player in the ring, then the first word of the game will go to player 1
            var currentPlayerIndex = players!.Count - 1;

            //Keep playing until we have a winner
            do
            {
                var movesToMake = OptimiseMoves();
                currentPlayerIndex = MakeMoves(movesToMake, currentPlayerIndex);
                currentPlayerIndex = RemovePlayer(currentPlayerIndex);
            } while (players!.Count > 1);

            var winner = players[0].PlayerNumber;
            logger.LogInformation("Winner is player {winner}", winner);
            return winner;
        }

        /// <summary>
        /// Initialise the game for the given number of players.
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        internal void InitialisePlayers(int numberOfPlayers)
        {
            players = new List<Player>();
            for (var index = 0; index < numberOfPlayers; index++)
            {
                players.Add(new Player { PlayerNumber = index + 1 });
            }
        }

        /// <summary>
        /// Calculate the number of moves to make, avoiding repeatedly looping around all remaining players.
        /// </summary>
        /// <returns>The optimum number of moves to make.</returns>
        internal int OptimiseMoves()
        {
            return numberOfWords % players!.Count;
        }

        /// <summary>
        /// Advance the game by the number of moves.
        /// </summary>
        /// <param name="numberOfMoves"></param>
        /// <param name="currentPlayerIndex"></param>
        /// <returns>The new player index.</returns>
        internal int MakeMoves(int numberOfMoves, int currentPlayerIndex)
        {
            var newIndex = currentPlayerIndex + numberOfMoves;
            if (newIndex > players!.Count - 1)
            {
                newIndex -= players.Count;
            }
            return newIndex;
        }

        /// <summary>
        /// Remove a player from the game.
        /// </summary>
        /// <param name="playerIndex"></param>
        /// <returns>The new player index.</returns>
        internal int RemovePlayer(int playerIndex)
        {
            var playerNumber = players![playerIndex].PlayerNumber;
            logger.LogInformation("Removing player {playerNumber}", playerNumber);
            players.RemoveAt(playerIndex);

            //Set the player index back one
            var newIndex = playerIndex - 1;
            if (newIndex < 0)
            {
                newIndex += players.Count;
            }
            return newIndex;
        }
    }
}
