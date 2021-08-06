using BoardCrud.Models;
using BoardCrud.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoardCrud_Test
{
    [TestClass]
    public class LogicTests
    {
        private static BoardService _BoardService;
        private static Player p1;
        private static Player p2;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _BoardService = new BoardService();
            p1 = new Player();
            p1.playerId = 1;
            p1.name = "John";
            p1.symbol = "X";
            p2 = new Player();
            p2.playerId = 2;
            p2.name = "Computer";
            p2.symbol = "O";
        }

        [TestMethod]
        public void TestGameBoardIsCreatedAndReturned()
        {
            GameBoard servicegb = _BoardService.CreateGameBoard();

            string[] matrix = new string[]{"0", "1", "2","3", "4", "5","6", "7", "8"};

            CollectionAssert.AreEqual(matrix, servicegb.BoardMatrix);
        }

        [TestMethod]
        public void TestGameStateIsCreatedAndReturned()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            Assert.AreEqual(gs.Player1, p1);
            Assert.AreEqual(gs.Player2, p2);
            Assert.IsFalse(gs.GameOver);
            Assert.IsNull(gs.Winner);
            Assert.AreEqual(gs.ActivePlayer, p1);
            CollectionAssert.AreEqual(gs.Board.BoardMatrix, new string[]{"0", "1", "2","3", "4", "5","6", "7", "8"});
        }

        [TestMethod]
        public void TestGameBoardIsUpdated()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);
            int movePosition = 4;

            GameState gbTest = _BoardService.PlayerMove(gs, movePosition);

            Assert.AreEqual("X", gbTest.Board.BoardMatrix[4]);
        }

        [TestMethod]
        public void TestGameBoardUpdatesActivePlayer()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 4);

            Assert.AreEqual(gbTest.ActivePlayer, p2);

            gbTest = _BoardService.PlayerMove(gs, 2);

            Assert.AreEqual(gbTest.ActivePlayer, p1);
        }

        [TestMethod]
        public void TestGameBoardUpdatesGameOverIfTrueRow1()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 0);
            gbTest = _BoardService.PlayerMove(gbTest, 3);
            gbTest = _BoardService.PlayerMove(gbTest, 1);
            gbTest = _BoardService.PlayerMove(gbTest, 4);
            gbTest = _BoardService.PlayerMove(gbTest, 2);

            Assert.IsTrue(gbTest.GameOver);
        }
        [TestMethod]
        public void TestGameBoardUpdatesGameOverIfTrueRow2()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 3);
            gbTest = _BoardService.PlayerMove(gbTest, 1);
            gbTest = _BoardService.PlayerMove(gbTest, 4);
            gbTest = _BoardService.PlayerMove(gbTest, 2);
            gbTest = _BoardService.PlayerMove(gbTest, 5);

            Assert.IsTrue(gbTest.GameOver);
        }

        [TestMethod]
        public void TestGameBoardUpdatesGameOverIfTrueRow3()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 6);
            gbTest = _BoardService.PlayerMove(gbTest, 3);
            gbTest = _BoardService.PlayerMove(gbTest, 7);
            gbTest = _BoardService.PlayerMove(gbTest, 4);
            gbTest = _BoardService.PlayerMove(gbTest, 8);

            Assert.IsTrue(gbTest.GameOver);
        }

        [TestMethod]
        public void TestGameBoardUpdatesGameOverIfTrueCol1()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 0);
            gbTest = _BoardService.PlayerMove(gbTest, 1);
            gbTest = _BoardService.PlayerMove(gbTest, 3);
            gbTest = _BoardService.PlayerMove(gbTest, 4);
            gbTest = _BoardService.PlayerMove(gbTest, 6);

            Assert.IsTrue(gbTest.GameOver);
        }

        [TestMethod]
        public void TestGameBoardUpdatesGameOverIfTrueCol2()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 1);
            gbTest = _BoardService.PlayerMove(gbTest, 0);
            gbTest = _BoardService.PlayerMove(gbTest, 4);
            gbTest = _BoardService.PlayerMove(gbTest, 8);
            gbTest = _BoardService.PlayerMove(gbTest, 7);

            Assert.IsTrue(gbTest.GameOver);
        }

        [TestMethod]
        public void TestGameBoardUpdatesGameOverIfTrueCol3()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 2);
            gbTest = _BoardService.PlayerMove(gbTest, 3);
            gbTest = _BoardService.PlayerMove(gbTest, 5);
            gbTest = _BoardService.PlayerMove(gbTest, 4);
            gbTest = _BoardService.PlayerMove(gbTest, 8);

            Assert.IsTrue(gbTest.GameOver);
        }

        [TestMethod]
        public void TestGameBoardUpdatesGameOverIfTrueUDD()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 0);
            gbTest = _BoardService.PlayerMove(gbTest, 3);
            gbTest = _BoardService.PlayerMove(gbTest, 4);
            gbTest = _BoardService.PlayerMove(gbTest, 5);
            gbTest = _BoardService.PlayerMove(gbTest, 8);

            Assert.IsTrue(gbTest.GameOver);
        }

        [TestMethod]
        public void TestGameBoardUpdatesGameOverIfTrueDUD1()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 2);
            gbTest = _BoardService.PlayerMove(gbTest, 3);
            gbTest = _BoardService.PlayerMove(gbTest, 4);
            gbTest = _BoardService.PlayerMove(gbTest, 5);
            gbTest = _BoardService.PlayerMove(gbTest, 6);

            Assert.IsTrue(gbTest.GameOver);
        }

        [TestMethod]
        public void TestGameBoardGameOverReturnsFalse()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 0);
            gbTest = _BoardService.PlayerMove(gbTest, 3);
            gbTest = _BoardService.PlayerMove(gbTest, 1);
            gbTest = _BoardService.PlayerMove(gbTest, 4);

            Assert.IsFalse(gbTest.GameOver);
        }

        [TestMethod]
        public void TestGameBoardGameOverReturnsTrueIsTurnIs9()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 0);
            gbTest = _BoardService.PlayerMove(gbTest, 1);
            gbTest = _BoardService.PlayerMove(gbTest, 2);
            gbTest = _BoardService.PlayerMove(gbTest, 4);
            gbTest = _BoardService.PlayerMove(gbTest, 3);
            gbTest = _BoardService.PlayerMove(gbTest, 5);
            gbTest = _BoardService.PlayerMove(gbTest, 7);
            gbTest = _BoardService.PlayerMove(gbTest, 6);
            gbTest = _BoardService.PlayerMove(gbTest, 8);

            Assert.IsTrue(gbTest.GameOver);
        }

        [TestMethod]
        public void TestGameStateWinnerIsNullIfDraw()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 0);
            gbTest = _BoardService.PlayerMove(gbTest, 1);
            gbTest = _BoardService.PlayerMove(gbTest, 2);
            gbTest = _BoardService.PlayerMove(gbTest, 4);
            gbTest = _BoardService.PlayerMove(gbTest, 3);
            gbTest = _BoardService.PlayerMove(gbTest, 5);
            gbTest = _BoardService.PlayerMove(gbTest, 7);
            gbTest = _BoardService.PlayerMove(gbTest, 6);
            gbTest = _BoardService.PlayerMove(gbTest, 8);

            Assert.IsNull(gbTest.Winner);
        }

        [TestMethod]
        public void TestGameBoardUpdatesWinner()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 0);
            gbTest = _BoardService.PlayerMove(gbTest, 3);
            gbTest = _BoardService.PlayerMove(gbTest, 1);
            gbTest = _BoardService.PlayerMove(gbTest, 4);
            gbTest = _BoardService.PlayerMove(gbTest, 2);

            Assert.AreEqual(gbTest.Winner, p1);
        }

        [TestMethod]
        public void TestGameBoardDoesNotUpdatesWinnerIfGameOverIsFalse()
        {
            GameState gs = _BoardService.CreateGameState(p1, p2);

            GameState gbTest = _BoardService.PlayerMove(gs, 0);
            gbTest = _BoardService.PlayerMove(gbTest, 3);
            gbTest = _BoardService.PlayerMove(gbTest, 1);
            gbTest = _BoardService.PlayerMove(gbTest, 4);

            Assert.IsNull(gbTest.Winner);
        }

    }
}