using System;

namespace BoardCrud.Models
{
    public class Player
    {
        public Guid playerId { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
    }
}