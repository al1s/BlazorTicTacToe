using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Core.Classes
{
    public class Cell
    {
        public int Position { get; set; }
        public char Symbol { get; set; } = default;
        public string View { get; set; } = string.Empty;
    }
}
