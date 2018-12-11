using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Core.Classes;

namespace TicTacToe.Core.Interfaces
{
    public interface IEngine
    {
        int MiniMax(Board board, Player player, int move);
        Tuple<bool, int> Utility(Board board, int move, char player);
        Task ChooseMove(Board board);
    }
}
