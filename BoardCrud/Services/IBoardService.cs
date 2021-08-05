using System.Threading.Tasks;
using BoardCrud.Models;

namespace BoardCrud.Services
{
    public interface IBoardService
    {
        GameBoard CreateGameBoard();
        GameState CreateGameState(Player p1, Player p2);
         
    }
}