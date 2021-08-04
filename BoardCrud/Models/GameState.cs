namespace BoardCrud.Models
{
    public class GameState
    {
        public int player1Id { get; set; }
        public int player2Id { get; set; }
        public int activePlayer { get; set; }
        public bool gameOver { get; set; }
    }
}