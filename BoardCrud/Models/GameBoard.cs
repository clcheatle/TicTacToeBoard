using System;

namespace BoardCrud.Models
{
    public class GameBoard
    {
        public int GameBoardId { get; set; }
        public string[] BoardMatrix { get; set; }
    }
}