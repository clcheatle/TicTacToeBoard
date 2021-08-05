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
            player1.playerId = 1;
            player1.name = "John";
            player1.symbol = "X";
            player2.playerId = 2;
            player2.name = "Jacky";
            player2.symbol = "O";
            gameboardTest.GameBoardId = 1;
            gameboardTest.BoardMatrix = new string[3,3]{{"-", "-", "-"},{"-", "-", "-"},{"-", "-", "-"}};
        }

        [TestMethod]
        public void TestPlayerModelCreatesSuccessfully()
        {  
            Assert.AreEqual(1, player1.playerId);
            Assert.AreEqual("John", player1.name);
            Assert.AreEqual("X", player1.symbol);
        }

        [TestMethod]
        public void TestGameBoardModelCreatesSuccessfully()
        {  
            GameBoard gameBoard = new GameBoard();
            gameBoard.GameBoardId = 1;
            gameBoard.BoardMatrix = new string[3,3]{{"-", "-", "-"},{"-", "-", "-"},{"-", "-", "-"}};
            CollectionAssert.AreEqual(new string[3,3]{{"-", "-", "-"},{"-", "-", "-"},{"-", "-", "-"}}, gameBoard.BoardMatrix);
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
            gb.GameBoardId = 1;
            gb.BoardMatrix = new string[3,3]{{"-", "-", "-"},{"-", "-", "-"},{"-", "-", "-"}};
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