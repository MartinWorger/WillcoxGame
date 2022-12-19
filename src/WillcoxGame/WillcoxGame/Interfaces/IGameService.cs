namespace WillcoxGame.Interfaces
{
    /// <summary>
    /// Service for playing the Willcox game.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Initialise the game.
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        /// <param name="numberOfWords"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        void InitialiseGame(int numberOfPlayers, int numberOfWords);

        /// <summary>
        /// Plays the game through to conclusion.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>The winning player number.</returns>
        int PlayGame();
    }
}
