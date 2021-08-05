namespace BoardCrud.Models
{
    public class GameState
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player ActivePlayer { get; set; }
        public bool GameOver { get; set; }
        public Player Winner { get; set; }
        public GameBoard Board { get; set; }
    }
}