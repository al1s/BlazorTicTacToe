using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTTGame.Server.DataAccess;
using TTTGame.Server.Models;
using TTTGame.Server.Models.Interfaces;

namespace TTTGame.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameStatsController : ControllerBase
    {
        private readonly GameStatsContext _context;
        private readonly IGameStats _gameStats;

        public GameStatsController(GameStatsContext context, IGameStats gameStats)
        {
            _context = context;
            _gameStats = gameStats;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<GameStats>> Get()
        {
            IEnumerable<GameStats> stats = await _gameStats.GetGames();
            return stats;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]GameStats game)
        {
            _gameStats.PostGame(game);
        }
    }
}
