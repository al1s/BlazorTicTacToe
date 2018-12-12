using Microsoft.EntityFrameworkCore;

namespace TTTGame.Server.DataAccess
{
    public class GameStatsContext : DbContext
    {
        public GameStatsContext(DbContextOptions<GameStatsContext> options) : base(options)
        {
        }

        
    }
}
