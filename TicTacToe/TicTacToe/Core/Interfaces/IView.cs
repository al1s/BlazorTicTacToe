using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Core.Classes;

namespace TicTacToe.Core.Interfaces
{
    public interface IView
    {
        void DrawBoard(Board board);
        void ToggleTurnIndicator(Player player);
    }
}
