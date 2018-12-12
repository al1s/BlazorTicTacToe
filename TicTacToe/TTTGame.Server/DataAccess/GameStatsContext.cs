using Microsoft.EntityFrameworkCore;
using TTTGame.Shared;

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

        /// <summary>
        /// Gets or sets statiscs of GameStats Model; Query and save instances
        /// </summary>
        public DbSet<GameStats> Stats { get; set; }
    }
}
