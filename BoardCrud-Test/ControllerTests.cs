using BoardCrud.Controllers;
using BoardCrud.Models;
using BoardCrud.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BoardCrud_Test
{
    [TestClass]
    public class ControllerTests
    {
        private Mock<IBoardService> mockBoardService;
        private BoardController boardController;
        private Player player1 = new Player();
        private Player player2 = new Player();

        public ControllerTests()
        {
            mockBoardService = new Mock<IBoardService>();
            boardController = new BoardController(mockBoardService.Object);
            player1.name = "John";
            player1.symbol = "X";
            player2.name = "Jacky";
            player2.symbol = "0";
        }
        
        [TestMethod]
        public void TestControllerCallsOnIBoardService()
        {
            GameStateDTO gsdto = new GameStateDTO();
            gsdto.Player1 = player1;
            gsdto.Player2 = player2;

            GameState gs = boardController.CreateGame(gsdto);
            mockBoardService.Verify(mock => mock.CreateGameState(player1,player2), Times.Once());
        }

        [TestMethod]
        public void TestControllerReturnsGameState()
        {
            GameStateDTO gsdto = new GameStateDTO();
            gsdto.Player1 = player1;
            gsdto.Player2 = player2;

            GameState gs = new GameState();
            gs.Player1 = player1;
            gs.Player2 = player2;
            gs.ActivePlayer = player1;
            gs.GameOver = false;
            gs.Winner = null;
            GameBoard gb = new GameBoard();
            gb.GameBoardId = new System.Guid("78da3f7b-6993-4113-9e10-7aea530e3711");
            gb.BoardMatrix = new string[]{"-", "-", "-","-", "-", "-","-", "-", "-"};
            gs.Board = gb;

            mockBoardService.Setup(mock => mock.CreateGameState(player1, player2)).Returns(gs);

            var gsTest = boardController.CreateGame(gsdto);
            Assert.AreEqual(gs, gsTest);
        }
    }
}
