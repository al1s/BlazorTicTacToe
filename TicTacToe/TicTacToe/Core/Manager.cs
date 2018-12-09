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
        private Board Board { get; set; }
        public void StartGameLoop() { }
        public void InitializeBoard() { }
        public Manager(IView view)
        {

        }
    }
}
