using System;

namespace BoardCrud.Models
{
    public class GameBoard
    {
        public Guid GameBoardId { get; set; }
        public string[] BoardMatrix { get; set; }
    }
}