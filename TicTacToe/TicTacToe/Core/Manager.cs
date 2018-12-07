using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Core.Classes;

namespace TicTacToe.Core
{
    public class Manager
    {
        public event EventHandler Updated;
        public void StartGameLoop() { }
        public void InitializeBoard() { }
    }
}
