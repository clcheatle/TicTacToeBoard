using System.Threading.Tasks;
using BoardCrud.Models;
using Microsoft.AspNetCore.Mvc;

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
            gb.BoardMatrix = new string[]{"0", "1", "2","3", "4", "5","6", "7", "8"};
            return gb;
        }

        public GameState PlayerMove(GameState gb, int movePosition)
        {
            gb.Board.BoardMatrix[movePosition] = gb.ActivePlayer.symbol;
            
            gb.GameOver = ThreeInRow(gb.Board);
            if(gb.GameOver) gb.Winner = gb.ActivePlayer;

            ChangeActivePlayer(gb);
            
            return gb;
        }

        private void ChangeActivePlayer(GameState gb)
        {
            if(gb.ActivePlayer == gb.Player1) 
            {
                gb.ActivePlayer = gb.Player2;
            }
            else
            {
                gb.ActivePlayer = gb.Player1;
            }
        }

        private bool ThreeInRow(GameBoard gb)
        {
            if(gb.BoardMatrix[0] == gb.BoardMatrix[1] && gb.BoardMatrix[0] == gb.BoardMatrix[2])
            {
                return true;
            }

            if(gb.BoardMatrix[3] == gb.BoardMatrix[4] && gb.BoardMatrix[3] == gb.BoardMatrix[5])
            {
                return true;
            }

            if(gb.BoardMatrix[6] == gb.BoardMatrix[7] && gb.BoardMatrix[6] == gb.BoardMatrix[8])
            {
                return true;
            }

            if(gb.BoardMatrix[0] == gb.BoardMatrix[3] && gb.BoardMatrix[0] == gb.BoardMatrix[6])
            {
                return true;
            }

            if(gb.BoardMatrix[1] == gb.BoardMatrix[4] && gb.BoardMatrix[1] == gb.BoardMatrix[7])
            {
                return true;
            }

            if(gb.BoardMatrix[2] == gb.BoardMatrix[5] && gb.BoardMatrix[2] == gb.BoardMatrix[8])
            {
                return true;
            }

            if(gb.BoardMatrix[0] == gb.BoardMatrix[4] && gb.BoardMatrix[0] == gb.BoardMatrix[8])
            {
                return true;
            }

            if(gb.BoardMatrix[2] == gb.BoardMatrix[4] && gb.BoardMatrix[2] == gb.BoardMatrix[6])
            {
                return true;
            }

            return false;
        }

    }
}