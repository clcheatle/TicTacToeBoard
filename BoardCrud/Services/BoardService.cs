using System.Threading.Tasks;
using BoardCrud.Models;

namespace BoardCrud.Services
{
    public class BoardService : IBoardService
    {
        public GameState CreateGameState(Player p1, Player p2)
        {
            GameState gs = new GameState();
            gs.Player1 = p1;
            gs.Player2 = p2;
            gs.ActivePlayer = p1;
            gs.Board = CreateGameBoard();

            return gs;
        }

        public GameBoard CreateGameBoard()
        {
            GameBoard gb = new GameBoard();
            gb.GameBoardId = new System.Guid();
            gb.BoardMatrix = new string[]{"-", "-", "-","-", "-", "-","-", "-", "-"};
            return gb;
        }

    }
}