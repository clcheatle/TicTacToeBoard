using BoardCrud.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoardCrud_Test
{
    [TestClass]
    public class ModelTests
    {
        Player player1 = new Player();
        Player player2 = new Player();
        GameBoard gameboardTest = new GameBoard();

        public ModelTests()
        {
            player1.playerId = new System.Guid("78da3f7b-6993-4113-9e10-7aea530e3711");
            player1.name = "John";
            player1.symbol = "X";
            player2.playerId = new System.Guid("78da3f7c-6993-4113-9e10-7aea530e3711");
            player2.name = "Jacky";
            player2.symbol = "O";
            gameboardTest.GameBoardId = new System.Guid("78da3f7b-6993-4113-9e10-7aea530e3711");
            gameboardTest.BoardMatrix = new string[]{"0", "1", "2","3", "4", "5","6", "7", "8"};
        }

        [TestMethod]
        public void TestPlayerModelCreatesSuccessfully()
        {  
            Assert.AreEqual(new System.Guid("78da3f7b-6993-4113-9e10-7aea530e3711"), player1.playerId);
            Assert.AreEqual("John", player1.name);
            Assert.AreEqual("X", player1.symbol);
        }

        [TestMethod]
        public void TestGameBoardModelCreatesSuccessfully()
        {  
            GameBoard gameBoard = new GameBoard();
            gameBoard.GameBoardId = new System.Guid("78da3f7b-6993-4113-9e10-7aea530e3711");
            gameBoard.BoardMatrix = new string[]{"0", "1", "2","3", "4", "5","6", "7", "8"};
            CollectionAssert.AreEqual(new string[]{"0", "1", "2","3", "4", "5","6", "7", "8"}, gameBoard.BoardMatrix);
        }

        [TestMethod]
        public void TestGameStateModelCreatesSuccessfully()
        {  
            GameState gs = new GameState();
            gs.Player1 = player1;
            gs.Player2 = player2;
            gs.ActivePlayer = player1;
            gs.GameOver = false;
            gs.Winner = null;
            GameBoard gb = new GameBoard();
            gb.GameBoardId = new System.Guid("78da3f7b-6993-4113-9e10-7aea530e3711");
            gb.BoardMatrix = new string[]{"0", "1", "2","3", "4", "5","6", "7", "8"};
            gs.Board = gb;

            Assert.AreEqual(player1, gs.Player1);
            Assert.AreEqual(player2, gs.Player2);
            Assert.AreEqual(player1, gs.ActivePlayer);
            Assert.AreEqual(false, gs.GameOver);
            Assert.IsNull(gs.Winner);
            Assert.AreEqual(gb, gs.Board);
        }
        
    }
}