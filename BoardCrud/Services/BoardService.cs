using System;
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
            gb.BoardMatrix = new char[]{'0', '1', '2','3', '4', '5','6', '7', '8'};
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
            gs.Board.BoardMatrix = new char[]{'0', '1', '2','3', '4', '5','6', '7', '8'};
            gs.Turn = 0;
            gs.ActivePlayer = gs.Player1;
            
            return gs;
        }

        public GameState ComputerMove(GameState gs)
        {
            gs.Board.BoardMatrix = BestComputerMove(gs);
            gs.Turn += 1;

            gs.GameOver = isGameOver(gs);
            
            ChangeActivePlayer(gs);

            return gs;
        }

        private static char[] BestComputerMove(GameState gs)
        {
            int bestValue = 1000;
            int[] move = new int [2];
            move[0] = -1;
            move[1] = -1;
            char p1 = gs.Player1.symbol;
            char p2 = gs.Player2.symbol;

            char[,] board = convertBoard(gs.Board);

            for(int i= 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(board[i,j] != 'X' && board[i,j] != 'O')
                    {
                        char temp = board[i,j];

                        board[i, j] = p2;

                        int moveValue = miniMax(board, 0, true, p1, p2);

                        board[i, j] = temp;

                        if(moveValue < bestValue)
                        {
                            move[0] = i;
                            move[1] = j;
                            bestValue = moveValue;
                        }
                    }
                }
            }

            board[move[0], move[1]] = p2;
            char[] revertedBoard = revertBoard(board);
            return revertedBoard;
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
            char[,] board = convertBoard(gb);
            if(RowWin(board) || ColWin(board) || DiagonalWin(board)) return true;

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

        private static bool RowWin(char[,] board)
        {
            if(board[0,0] == board[0,1] && board[0,0] == board[0,2]
            || board[1,0] == board[1,1] && board[1,0] == board[1,2]
            || board[2,0] == board[2,1] && board[2,0] == board[2,2])
            {
                return true;
            }

            return false;
        }

        private static bool ColWin(char[,] board)
        {
            if(board[0,0] == board[1,0] && board[0,0] == board[2,0]
            || board[0,1] == board[1,1] && board[0,1] == board[2,1]
            || board[0,2] == board[1,2] && board[0,2] == board[2,2])
            {
                return true;
            }

            return false;
        }

        private static bool DiagonalWin(char[,] board)
        {
            if(board[0,0] == board[1,1] && board[0,0] == board[2,2]
            || board[2,0] == board[1,1] && board[2,0] == board[0,2])
            {
                return true;
            }

            return false;
        }

        private static char[,] convertBoard(GameBoard gameBoard)
        {
            char[,] newBoard = new char[3,3];
            int counter = 0;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    newBoard[i,j] = gameBoard.BoardMatrix[counter];
                    counter++;
                }
            }

            return newBoard; 
        }

        private static char[] revertBoard(char[,] board)
        {
            char[] newBoard = new char[9];
            int k = 0;

            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    newBoard[k] = board[i, j];
                    k++;
                }
            }

            return newBoard;
        }

        private static int evaluateRowScore(char[,] board, char p1, char p2)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] &&
                    board[i, 1] == board[i, 2])
                {
                    if (board[i, 0] == p1)
                        return +10;
                    else if (board[i, 0] == p2)
                        return -10;
                }
            }

            return 0;
        }

        private static int evaluateColScore(char[,] board, char p1, char p2)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] == board[1, i] &&
                    board[1, i] == board[2, i])
                {
                    if (board[0, i] == p1)
                        return +10;
                    else if (board[0, i] == p2)
                        return -10;
                }
            }

            return 0;
        }

        private static int evaluateDiagonalScore(char[,] board, char p1, char p2)
        {
            // Checking for Diagonals for X or O victory.
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                if (board[0, 0] == p1)
                    return +10;
                else if (board[0, 0] == p2)
                    return -10;
            }
        
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                if (board[0, 2] == p1)
                    return +10;
                else if (board[0, 2] == p2)
                    return -10;
            }

            return 0;
        }

        private static int evaluateScore(char[,] board, char p1, char p2)
        {
            int rowScore = evaluateRowScore(board, p1, p2);
            int colScore = evaluateColScore(board, p1, p2);
            int diagonalScore = evaluateDiagonalScore(board, p1, p2);

            if(rowScore != 0)
                return rowScore;
            if(colScore != 0)
                return colScore;
            if(diagonalScore != 0)
                return diagonalScore;

            return 0;
        }

        private static int miniMax(char[,] board, int depth, bool isMax, char p1, char p2)
        {
            int score = evaluateScore(board, p1, p2);

            if(score == 10)
                return score;
            
            if(score == -10)
                return score;
            
            if(isMovesLeft(board) == false)
                return 0;
            
            if(isMax)
            {
                int best = -1000;

                for(int i = 0; i < 3; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        if(board[i,j] != 'X' && board[i,j] != 'O')
                        {
                            char temp = board[i,j];

                            board[i,j] = p1;

                            best = Math.Max(best, miniMax(board, depth + 1, !isMax, p1, p2));

                            board[i, j] = temp;
                        }
                    }
                }

                return best;
            }

            else
            {
                int best = 1000;

                for(int i = 0; i < 3; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        if(board[i,j] != 'X' && board[i,j] != 'O')
                        {
                            char temp = board[i,j];

                            board[i,j] = p2;

                            best = Math.Min(best, miniMax(board, depth + 1, !isMax, p1, p2));

                            board[i, j] = temp;
                        }
                    }
                }
                return best;
            }
        }

        private static bool isMovesLeft(char [,] board)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] != 'X' && board[i,j] != 'O')
                        return true;
            return false;
        }

    }
}