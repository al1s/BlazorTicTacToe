using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Core.Classes;

namespace TicTacToe.Core
{
    public class UI
    {
        private static BoardViewModel _currentBoard { get; set; }
        private static UI _ui { get; set; }
        private static Manager _manager { get; set; }
        private UI()
        {
            _currentBoard = new BoardViewModel();
        }
        public static UI Instance
        {
            get {
                if(_ui == null)
                {
                    _ui = new UI();
                }
                return _ui;
            }

        }
        public BoardViewModel CurrentBoard
        {
            get {
                return _currentBoard;
            }
        }
        public Manager Manager
        {
            get
            {
                return _manager;
            }
        }
    }
}
