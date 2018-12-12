using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTTGame.Server.DataAccess
{
    public class GameStatsContext : DbContext
    {
        public GameStatsContext(DbContextOptions<GameStatsContext> options) : base(options)
        {
        }
    }
}
