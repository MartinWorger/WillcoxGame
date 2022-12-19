using Microsoft.Extensions.Logging;
using WillcoxGame.GamePlay;

namespace WillcoxGameTests
{
    [TestClass]
    public class GameServiceTests
    {
        Mock<ILogger<GameService>>? mockLogger;
        
        GameService NewGameService() =>
            new(mockLogger!.Object);

        [TestInitialize]
        public void Initialise()
        {
            mockLogger = new Mock<ILogger<GameService>>();
        }

        [DataTestMethod]
        [DataRow(-1, 10)]
        [DataRow(0, 10)]
        [DataRow(1, 10)]
        [DataRow(10, -1)]
        [DataRow(10, 0)]
        [DataRow(10, 1)]
        public void InitialiseGame_WithInvalidParams_Throws(int numberOfPlayers, int numberOfWords)
        {
            // Arrange
            var service = NewGameService();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                service.InitialiseGame(numberOfPlayers, numberOfWords)
            );
        }

        [TestMethod]
        public void InitialiseGame_WithValidParams_DoesNotThrow()
        {
            // Arrange
            var service = NewGameService();

            // Act & Assert
            service.InitialiseGame(2, 2);

            // Assert
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void PlayGame_WhenNotInitialised_Throws()
        {
            // Arrange
            var service = NewGameService();

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() =>
                service.PlayGame()
            );
        }

        [DataTestMethod]
        [DataRow(2, 2, 1)]
        [DataRow(2, 3, 2)]
        [DataRow(3, 2, 3)]
        [DataRow(2, 8, 1)]
        [DataRow(3, 8, 3)]
        [DataRow(6, 3, 1)]
        [DataRow(7, 3, 4)]
        [DataRow(23, 7, 1)]
        public void PlayGame_ReturnsExpectedWinner(int numberOfPlayers, int numberOfWords, int expectedWinner)
        {
            // Arrange
            var service = NewGameService();
            service.InitialiseGame(numberOfPlayers, numberOfWords);

            // Act
            var result = service.PlayGame();

            // Assert
            Assert.AreEqual(expectedWinner, result);
        }

    }
}