using BoardCrud.Models;
using BoardCrud.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoardCrud_Test
{
    [TestClass]
    public class LogicTests
    {
        private static BoardService _BoardService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _BoardService = new BoardService();
        }

        [TestMethod]
        public void TestGameBoardIsCreatedAndReturned()
        {
            GameBoard servicegb = _BoardService.CreateGameBoard();

            string[,] matrix = new string[3,3]{{"-", "-", "-"},{"-", "-", "-"},{"-", "-", "-"}};

            CollectionAssert.AreEqual(matrix, servicegb.BoardMatrix);
        }

        [TestMethod]
        public void TestGameStateIsCreatedAndReturned()
        {
            Player p1 = new Player();
            p1.name = "John";
            p1.symbol = "X";
            Player p2 = new Player();
            p2.name = "Jacky";
            p2.symbol = "0";

            GameState gs = _BoardService.CreateGameState(p1, p2);

            Assert.AreEqual(gs.Player1, p1);
            Assert.AreEqual(gs.Player2, p2);
            Assert.IsFalse(gs.GameOver);
            Assert.IsNull(gs.Winner);
            Assert.AreEqual(gs.ActivePlayer, p1);
            CollectionAssert.AreEqual(gs.Board.BoardMatrix, new string[3,3]{{"-", "-", "-"},{"-", "-", "-"},{"-", "-", "-"}});
        }
    }
}