using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTTGame.Server.Models;

namespace TTTGame.Server.DataAccess
{
    public class GameStatsContext : DbContext
    {
        /// <summary>
        /// Initialize instance of GameStatsContext Class
        /// </summary>
        /// <param name="options"></param>
        public GameStatsContext(DbContextOptions<GameStatsContext> options) : base(options)
        {
        }

        public DbSet<GameStats> GameStats { set; get; }
    }
}
