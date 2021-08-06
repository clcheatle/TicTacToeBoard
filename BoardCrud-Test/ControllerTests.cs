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
            GameStateDto gsdto = new GameStateDto();
            gsdto.Player1 = player1;
            gsdto.Player2 = player2;

            GameState gs = boardController.CreateGame(gsdto);
            mockBoardService.Verify(mock => mock.CreateGameState(player1,player2), Times.Once());
        }

        [TestMethod]
        public void TestControllerReturnsNewGameState()
        {
            GameStateDto gsdto = new GameStateDto();
            gsdto.Player1 = player1;
            gsdto.Player2 = player2;

            GameState gs = new GameState();
            gs.Player1 = player1;
            gs.Player2 = player2;
            gs.ActivePlayer = player1;
            gs.GameOver = false;
            gs.Winner = null;
            GameBoard gb = new GameBoard();
            gb.GameBoardId = 1;
            gb.BoardMatrix = new string[]{"0", "1", "2","3", "4", "5","6", "7", "8"};
            gs.Board = gb;

            mockBoardService.Setup(mock => mock.CreateGameState(player1, player2)).Returns(gs);

            var gsTest = boardController.CreateGame(gsdto);
            Assert.AreEqual(gs, gsTest);
        }

        [TestMethod]
        public void TestBoardControllerCallsPlayerMoveService()
        {
            GameState gs = new GameState();
            gs.Player1 = player1;
            gs.Player2 = player2;
            gs.ActivePlayer = player1;
            gs.GameOver = false;
            gs.Winner = null;
            GameBoard gb = new GameBoard();
            gb.GameBoardId = 1;
            gb.BoardMatrix = new string[]{"O", "O", "2","X", "X", "5","6", "7", "8"};
            gs.Board = gb;

            Move m = new Move();
            m.GameState = gs;
            m.MovePosition = 5;
            GameState gsTest = boardController.PlayerMove(m);
            mockBoardService.Verify(mock => mock.PlayerMove(gs,5), Times.Once());

        }

        [TestMethod]
        public void TestBoardControllerReturnsGameState()
        {
            GameState gs = new GameState();
            gs.Player1 = player1;
            gs.Player2 = player2;
            gs.ActivePlayer = player1;
            gs.GameOver = false;
            gs.Winner = null;
            GameBoard gb = new GameBoard();
            gb.GameBoardId = 1;
            gb.BoardMatrix = new string[]{"O", "O", "2","X", "X", "5","6", "7", "8"};
            gs.Board = gb;

            GameState updatedGS = gs;
            updatedGS.Winner = player1;
            updatedGS.GameOver = true;
            updatedGS.Board.BoardMatrix[5] = "X";

            Move m = new Move();
            m.GameState = gs;
            m.MovePosition = 5;

            mockBoardService.Setup(mock => mock.PlayerMove(gs, 5)).Returns(updatedGS);

            GameState gsTest = boardController.PlayerMove(m);
            Assert.AreEqual(gs, gsTest);
        }

        public void TestBoardControllerCallsResetService()
        {
            GameState gs = new GameState();
            gs.Player1 = player1;
            gs.Player2 = player2;
            gs.ActivePlayer = player1;
            gs.GameOver = false;
            gs.Winner = null;
            GameBoard gb = new GameBoard();
            gb.GameBoardId = 1;
            gb.BoardMatrix = new string[]{"O", "O", "2","X", "X", "5","6", "7", "8"};
            gs.Board = gb;

            GameState gsTest = boardController.ResetGame(gs);
            mockBoardService.Verify(mock => mock.ResetGameState(gs), Times.Once());

        }

        [TestMethod]
        public void TestBoardControllerResetsGameState()
        {
            GameState gs = new GameState();
            gs.Player1 = player1;
            gs.Player2 = player2;
            gs.ActivePlayer = player1;
            gs.GameOver = false;
            gs.Winner = null;
            GameBoard gb = new GameBoard();
            gb.GameBoardId = 1;
            gb.BoardMatrix = new string[]{"O", "O", "2","X", "X", "5","6", "7", "8"};
            gs.Board = gb;

            GameState updatedGS = gs;
            updatedGS.Winner = null;
            updatedGS.GameOver = false;
            updatedGS.Turn = 0;
            updatedGS.Board.BoardMatrix = new string[]{"0", "1", "2","3", "4", "5","6", "7", "8"};

            mockBoardService.Setup(mock => mock.ResetGameState(gs)).Returns(updatedGS);

            GameState gsTest = boardController.ResetGame(gs);
            Assert.AreEqual(gs, gsTest);
        }
    }
}
