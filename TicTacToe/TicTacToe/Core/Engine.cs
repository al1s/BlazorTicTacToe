using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Core.Classes;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core
{
    public class Engine : IEngine
    {
        public event EventHandler Updated;
        private IManager _manager;
        public Engine(IManager manager)
        {
            _manager = manager;
        }
        public Task ChooseMove(Board board)
        {
            throw new NotImplementedException();
        }

        public int MiniMax(Board board, Player player, int move)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, int> Utility(Board board, int move, Player player)
        {
            throw new NotImplementedException();
        }
    }
}
