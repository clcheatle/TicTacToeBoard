using System;

namespace BoardCrud.Models
{
    public class GameBoard
    {
        public int GameBoardId { get; set; }
        public char[] BoardMatrix { get; set; }
    }
}