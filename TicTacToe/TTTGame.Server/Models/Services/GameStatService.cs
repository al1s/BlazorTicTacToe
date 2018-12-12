using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTTGame.Server.DataAccess;
using TTTGame.Server.Models.Interfaces;

namespace TTTGame.Server.Models.Services
{
    class GameStatService : IGameStats
    {
        private GameStatsContext _context;

        public GameStatService(GameStatsContext context)
        {
            _context = context;
        }

        public async Task PostGame(GameStats game)
        {
            _context.GameStats.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GameStats>> GetGames()
        {
            return await _context.GameStats.ToListAsync();
        }
    }
}
