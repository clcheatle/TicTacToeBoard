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
            gs.Turn = 0;

            return gs;
        }

        public GameBoard CreateGameBoard()
        {
            GameBoard gb = new GameBoard();
            gb.BoardMatrix = new string[]{"0", "1", "2","3", "4", "5","6", "7", "8"};
            return gb;
        }

        public GameState PlayerMove(GameState gs, int movePosition)
        {
            gs.Board.BoardMatrix[movePosition] = gs.ActivePlayer.symbol;
            gs.Turn += 1;
            
            gs.GameOver = isGameOver(gs);

            ChangeActivePlayer(gs);
            
            return gs;
        }

        public GameState ResetGameState(GameState gs)
        {
            gs.Board.BoardMatrix = new string[]{"0", "1", "2", "3", "4", "5", "6", "7", "8"};
            gs.Turn = 0;
            gs.ActivePlayer = gs.Player1;
            
            return gs;
        }

        private static void ChangeActivePlayer(GameState gb)
        {
            if(gb.ActivePlayer.playerId == gb.Player1.playerId) 
            {
                gb.ActivePlayer = gb.Player2;
            }
            else
            {
                gb.ActivePlayer = gb.Player1;
            }
        }

        private static bool ThreeInRow(GameBoard gb)
        {
            
            if(RowWin(gb) || ColWin(gb) || DiagonalWin(gb)) return true;

            return false;
        }

        private static bool isGameOver (GameState gs)
        {
            if(ThreeInRow(gs.Board))
            {
                gs.Winner = gs.ActivePlayer;
                return true;
            }
            
            if(gs.Turn >= 9)
            {
                return true;
            }

            return false;
        }

        private static bool RowWin(GameBoard gb)
        {
            if(gb.BoardMatrix[0] == gb.BoardMatrix[1] && gb.BoardMatrix[0] == gb.BoardMatrix[2]
            || gb.BoardMatrix[3] == gb.BoardMatrix[4] && gb.BoardMatrix[3] == gb.BoardMatrix[5]
            || gb.BoardMatrix[6] == gb.BoardMatrix[7] && gb.BoardMatrix[6] == gb.BoardMatrix[8])
            {
                return true;
            }

            return false;
        }

        private static bool ColWin(GameBoard gb)
        {
            if(gb.BoardMatrix[0] == gb.BoardMatrix[3] && gb.BoardMatrix[0] == gb.BoardMatrix[6]
            || gb.BoardMatrix[1] == gb.BoardMatrix[4] && gb.BoardMatrix[1] == gb.BoardMatrix[7]
            || gb.BoardMatrix[2] == gb.BoardMatrix[5] && gb.BoardMatrix[2] == gb.BoardMatrix[8])
            {
                return true;
            }

            return false;
        }

        private static bool DiagonalWin(GameBoard gb)
        {
            if(gb.BoardMatrix[0] == gb.BoardMatrix[4] && gb.BoardMatrix[0] == gb.BoardMatrix[8]
            || gb.BoardMatrix[2] == gb.BoardMatrix[4] && gb.BoardMatrix[2] == gb.BoardMatrix[6])
            {
                return true;
            }

            return false;
        }
    }
}