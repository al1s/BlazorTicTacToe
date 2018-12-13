using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TTTGame.Server.Models.Interfaces
{
    public interface IGameStats
    {
        Task<IEnumerable<GameStats>> GetGames();

        Task PostGame(GameStats game);
    }
}
