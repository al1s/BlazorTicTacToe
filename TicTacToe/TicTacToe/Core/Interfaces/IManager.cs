using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Core.Interfaces
{
    public interface IManager
    {
        void InitializeBoard(int boardSize);
        char GetMinPlayer { get; }
        char GetMaxPlayer { get; }
    }
}
